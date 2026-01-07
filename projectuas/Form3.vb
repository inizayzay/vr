Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports MySql.Data.MySqlClient ' Diperlukan untuk interaksi DB

Public Class Form3
    ' Mantra untuk Gradasi Background
    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        Using brush As New Drawing2D.LinearGradientBrush(ClientRectangle,
                                                     Color.FromArgb(108, 92, 231), ' Ungu Utama
                                                     Color.FromArgb(162, 155, 254), ' Ungu Muda/Pink
                                                     90.0F) ' Sudut Miring
            e.Graphics.FillRectangle(brush, ClientRectangle)
        End Using
    End Sub
    ' =======================================================
    ' VARIABEL UTAMA
    ' =======================================================
    Private _namaPengguna As String
    Private _teksTarget As String
    Private _teksDiucapkan As String
    Private _userId As Integer      ' Diterima dari Form2
    Private _questionId As Integer  ' Diterima dari Form2
    Private _scorePersen As Double  ' Skor tes saat ini (0.00 - 100.00)

    ' NEW: DTW Scoring
    Private _userWavPath As String  ' Path to recorded audio
    Private _scoreFA As Double      ' Force Alignment Score (0-100)
    Private _wordDetails As JArray  ' Detailed scores per word
    Private _duration As Double    ' Speech duration
    Private _combinedScore As Double ' Final score
    Private _missingWords As New List(Of String)
    Private _extraWords As New List(Of String)
    Private _transcriptionAI As String = "" ' NEW: Transcription from AI

    ' --- Deklarasi Komponen (Asumsi default names) ---
    ' Label1 = Your score (Kita akan gunakan ini untuk Total Kumulatif)
    ' Label2 = 100 (Kita akan gunakan ini untuk Skor Tes SAAT INI)
    ' Label3 = Good Job !
    ' Button1 = Try Again
    ' Button2 = Exit

    ' =======================================================
    ' 1. CONSTRUCTOR
    ' =======================================================
    Public Sub New(ByVal nama As String, ByVal target As String, ByVal diucapkan As String,
                   ByVal userId As Integer, ByVal questionId As Integer, ByVal wavPath As String)
        InitializeComponent()
        _namaPengguna = nama
        _teksTarget = target
        _teksDiucapkan = diucapkan
        _userId = userId
        _questionId = questionId
        _userWavPath = wavPath ' NEW
    End Sub

    ' =======================================================
    ' 2. FORM LOAD (UPDATED TO ASYNC)
    ' =======================================================
    Private Async Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Pronunciation Test Results"
        Me.StartPosition = FormStartPosition.CenterScreen


        ' 0. UPDATE HEADER GREETING (Label5) - SEGERA AGAR TIDAK MUNCUL "User"
        Dim xpData = DatabaseModule.GetUserXPAndLevel(_userId)
        Dim category = DatabaseModule.GetUserCategory(xpData.Item1)
        Label5.Text = $"HI, {_namaPengguna} ({category}) [Lv {xpData.Item1}]"

        CenterCard() ' Posisi awal

        ' Tampilkan status loading sementara (Opsional)
        Label2.Text = "Calculating..."
        Label3.Text = "Please wait while System analyzes your voice..."

        ' 1. HITUNG SKOR FORCE ALIGNMENT (ASYNC)
        ' Ini akan menghubungi Python API dan mengisi _scoreFA, _wordDetails, dan _duration
        _scoreFA = Await CalculateFASimilarityAsync()
        _combinedScore = _scoreFA

        ' 2. HITUNG WORD ACCURACY DARI SPEECH RECOGNIZER (Opsional/Pembanding)
        _scorePersen = HitungAkurasi(_teksTarget, _teksDiucapkan)

        ' 3. SIMPAN HASIL KE DATABASE
        If _userId > 0 AndAlso Not _teksDiucapkan.StartsWith("[") Then
            ' Map _scoreFA ke kolom dtw_score dan final_score untuk kompatibilitas data lama
            SaveResultToDatabase(_userId, _questionId, _combinedScore, _scoreFA, _scorePersen, _teksDiucapkan)
        End If

        ' 4. TAMPILKAN SKOR (Label2)
        Label2.Text = $"{_combinedScore:N2}%"
        Label2.ForeColor = If(_combinedScore >= 80, Color.Green, Color.Red)

        ' 6. TAMPILKAN PESAN FEEDBACK (Label3)
        UpdateFeedbackMessage()

        ' 7. TAMPILKAN TOTAL SKOR KUMULATIF (Label1)
        Dim totalScoreText As String = GetCumulativeScore(_userId)
        Label1.Text = $"Your total score: {totalScoreText}"

        ' 8. TAMPILKAN DETAIL EVALUASI (Label6)
        UpdateDetailedEvaluation()
    End Sub

    Private Sub UpdateDetailedEvaluation()
        ' Tentukan Grade berdasarkan Combined Score
        Dim grade As String = "D"
        Dim saran As String = ""
        Dim gradeColor As Color


        If _combinedScore >= 95 Then
            grade = "A+" : gradeColor = Color.Gold : saran = "Excellent! Your pronunciation is nearly perfect."
        ElseIf _combinedScore >= 85 Then
            grade = "A" : gradeColor = Color.Green : saran = "Very good! Maintain your fluency."
        ElseIf _combinedScore >= 70 Then
            grade = "B" : gradeColor = Color.Blue : saran = "Good, but pay closer attention to your intonation."
        ElseIf _combinedScore >= 50 Then
            grade = "C" : gradeColor = Color.Orange : saran = "Fair. You need more practice with articulation."
        Else
            grade = "D" : gradeColor = Color.Red : saran = "Don't give up! Try listening to the reference more carefully."
        End If

        CenterCard() ' Pastikan panel & tombol di posisi yang benar

        ' Atur Posisi Tombol & XP agar Fixed di Bawah Panel2
        Button1.Top = scrollPanel.Height - 65
        Button2.Top = scrollPanel.Height - 65
        lblXP.Top = scrollPanel.Height - 35

        ' Update lblXP dengan data asli
        Dim xpData = DatabaseModule.GetUserXPAndLevel(_userId)
        lblXP.Text = $"Level {xpData.Item1} | {xpData.Item2:N0} XP"
        lblXP.Left = (scrollPanel.Width - lblXP.Width) \ 2 ' Center it below buttons

        ' Pastikan tombol & XP ada di depan (Z-Order)
        Button1.BringToFront()
        Button2.BringToFront()
        lblXP.BringToFront()


        ' Bangun string detail evaluasi yang LEBIH RAPI & DETAIL
        Dim detail As New System.Text.StringBuilder()
        detail.AppendLine("--- EVALUATION DETAIL (FORCE ALIGNMENT) ---")
        detail.AppendLine($"Target: {_teksTarget.ToUpper()}")
        detail.AppendLine($"Heard : {If(String.IsNullOrEmpty(_transcriptionAI), _teksDiucapkan, _transcriptionAI).ToUpper()}")
        detail.AppendLine("")

        detail.AppendLine($"OVERALL SCORE: {_scoreFA:N2}%")
        detail.AppendLine($"Speech Duration   : {_duration:N2} seconds")
        detail.AppendLine("")

        detail.AppendLine("DETAIL PER WORD & PHONEMES:")
        If _wordDetails IsNot Nothing Then
            For Each wordItem In _wordDetails
                Dim w = wordItem("word").ToString()
                Dim s = wordItem("score").ToString()
                Dim st = wordItem("status").ToString()
                ' Translate status if needed (though API returns English)
                detail.AppendLine($"- {w.ToUpper().PadRight(12)}: {s}% ({st})")

                ' Tampilkan detail fonem (bunyi) - VERSI ANAK-ANAK
                Dim phonemes = wordItem("phonemes")
                If phonemes IsNot Nothing Then
                    Dim pList As New List(Of String)
                    For Each pItem In phonemes
                        Dim p = pItem("phoneme").ToString().Replace("0", "").Replace("1", "").Replace("2", "")
                        Dim pSt = pItem("status").ToString()

                        If pSt = "Good" Then
                            pList.Add("[" & p & " ✅]")
                        ElseIf pSt = "Missing" Then
                            pList.Add("[" & p & " ❌]")
                        Else
                            pList.Add("[" & p & " ✨]")
                        End If
                    Next
                    detail.AppendLine($"  Audio: {String.Join("  ", pList)}")
                End If
                detail.AppendLine("----------------------------------------")
            Next
        End If

        ' Tambahkan Legenda Sederhana untuk Anak
        detail.AppendLine("Legend:")
        detail.AppendLine("✅ = Your voice is great!")
        detail.AppendLine("✨ = Let's try this part again to make it even better!")

        detail.AppendLine("")
        detail.AppendLine($"GRADE: {grade}")
        detail.AppendLine($"ADVICE: {saran}")

        Label6.Text = detail.ToString()
        Label6.Font = New Font("Consolas", 9, FontStyle.Bold)
        Label6.ForeColor = Color.DarkSlateBlue
        Label6.BackColor = Color.FromArgb(240, 240, 240) ' Latar belakang terang agar kontras
        Label6.AutoSize = True
        ' Label6.MaximumSize = New Size(scrollPanel.Width - 30, 0)
    End Sub

    Private Sub UpdateFeedbackMessage()
        If _combinedScore >= 95.0 Then
            Label3.Text = "Excellent! Your pronunciation is very accurate."
        ElseIf _combinedScore >= 80.0 Then
            Label3.Text = "Great! Keep maintaining your pronunciation."
        ElseIf _combinedScore >= 60.0 Then
            Label3.Text = "Good, but still needs a lot of practice."
        Else
            Label3.Text = "Don't give up! Try listening to the sample again."
        End If
    End Sub

    ' =======================================================
    ' 2. FUNGSI HITUNG FORCE ALIGNMENT VIA API (ASYNC)
    ' =======================================================
    Private Async Function CalculateFASimilarityAsync() As Task(Of Double)
        Try
            Debug.WriteLine("=== FA Calculation Start (API Async) ===") ' Diperbarui


            If String.IsNullOrEmpty(_userWavPath) OrElse Not System.IO.File.Exists(_userWavPath) Then
                Debug.WriteLine("ERROR: No audio file recorded or file not found")
                MessageBox.Show("Audio file not found. FA score will be 0.", "Audio Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning) ' Diperbarui
                Return 0.0
            End If

            ' CALL THE PYTHON API (Force Alignment)
            Dim resultObj As JObject = Await MFCCExtractor.GetAPIResultAsync(_userWavPath, _teksTarget)


            _scoreFA = CDbl(resultObj("score"))
            _wordDetails = CType(resultObj("word_details"), JArray)
            _duration = CDbl(resultObj("duration"))
            _transcriptionAI = resultObj("transcription").ToString() ' NEW

            Debug.WriteLine($"FA Score: {_scoreFA:F2}%")
            Debug.WriteLine($"Duration: {_duration:F2}")
            Debug.WriteLine("=== Force Alignment End ===")

            Return _scoreFA

        Catch ex As Exception
            Debug.WriteLine($"EXCEPTION in CalculateDTWSimilarity (API): {ex.Message}")


            ' Tampilkan pesan error jika server tidak jalan
            MessageBox.Show($"Failed to connect to Python API for scoring.{vbCrLf}" &
                           $"Make sure 'api_server.py' is running.{vbCrLf}{vbCrLf}" &
                           $"Error Details: {ex.Message}", "API Connection Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning)


            Return 0.0
        End Try
    End Function

    ' =======================================================
    ' 3. FUNGSI SIMPAN HASIL KE DATABASE (Tabel: results)
    ' =======================================================
    Private Sub SaveResultToDatabase(ByVal userId As Integer, ByVal questionId As Integer,
                                      ByVal finalScore As Double, ByVal dtwScore As Double,
                                      ByVal wordAccuracy As Double, ByVal spokenText As String)
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseModule.GetConnection()
            If conn Is Nothing Then Exit Sub

            Dim sql As String = "INSERT INTO results (user_id, question_id, spoken_text, word_accuracy, phoneme_accuracy, final_score, dtw_score, audio_file_path) " &
                                "VALUES (@uid, @qid, @spokenText, @wordAcc, @phoneAcc, @finalScore, @dtwScore, @audioPath)"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@uid", userId)
            cmd.Parameters.AddWithValue("@qid", questionId)
            cmd.Parameters.AddWithValue("@spokenText", spokenText)
            cmd.Parameters.AddWithValue("@wordAcc", wordAccuracy)
            cmd.Parameters.AddWithValue("@phoneAcc", 0.0) ' Placeholder
            cmd.Parameters.AddWithValue("@finalScore", finalScore)
            cmd.Parameters.AddWithValue("@dtwScore", dtwScore)
            cmd.Parameters.AddWithValue("@audioPath", _userWavPath)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show($"Failed to save results to database: {ex.Message}", "DB Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DatabaseModule.CloseConnection(conn)
        End Try
    End Sub

    ' =======================================================
    ' 4. FUNGSI AMBIL SKOR KUMULATIF (Ditampilkan di Label1)
    ' =======================================================
    Private Function GetCumulativeScore(ByVal userId As Integer) As String
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseModule.GetConnection()
            If conn Is Nothing Then Return "N/A (DB Error)"

            ' Query untuk menghitung rata-rata skor total dan jumlah tes
            Dim sql As String = "SELECT AVG(final_score), COUNT(id) FROM results WHERE user_id = @uid"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@uid", userId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim avgScore As Double = 0.0
                Dim testCount As Integer = 0

                If Not reader.IsDBNull(0) Then avgScore = reader.GetDouble(0)
                If Not reader.IsDBNull(1) Then testCount = reader.GetInt32(1)

                reader.Close()

                If testCount > 0 Then
                    Return $"Average: {avgScore:N2}% ({testCount} tests)"
                Else
                    Return "No previous scores found."
                End If
            Else
                reader.Close()
                Return "N/A (No results)"
            End If

        Catch ex As Exception
            Return $"N/A (Error: {ex.Message})"
        Finally
            DatabaseModule.CloseConnection(conn)
        End Try
    End Function


    ' =======================================================
    ' 5. FUNGSI PERHITUNGAN AKURASI (Word Intersection)
    ' (Harus sama dengan yang Anda gunakan di project Anda)
    ' =======================================================
    Private Function HitungAkurasi(ByVal target As String, ByVal actual As String) As Double
        _missingWords.Clear()
        _extraWords.Clear()

        ' 1. Fungsi pembersihan string
        Dim CleanString = Function(s As String) As String
                              Return New String(s.Where(Function(c) Char.IsLetterOrDigit(c) OrElse Char.IsWhiteSpace(c)).ToArray()).ToLower()
                          End Function

        ' 2. Tokenisasi
        Dim targetWords As String() = CleanString(target).Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim actualWords As String() = CleanString(actual).Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        If targetWords.Length = 0 Then Return 0

        ' 3. Hitung kecocokan
        Dim targetList As List(Of String) = targetWords.ToList()
        Dim actualList As List(Of String) = actualWords.ToList()
        Dim correctCount As Integer = 0

        ' Cek kata yang benar
        For Each word In targetWords
            If actualList.Contains(word) Then
                correctCount += 1
                actualList.Remove(word) ' Hapus agar tidak dihitung dua kali
            Else
                _missingWords.Add(word) ' Kata yang ada di target tapi tidak diucap
            End If
        Next

        ' Kata yang tersisa di actualList adalah kata tambahan (extra)
        _extraWords.AddRange(actualList)

        ' 5. Hitung skor
        Dim finalScore As Double = (CDbl(correctCount) / targetWords.Length) * 100
        Return Math.Min(100.0, finalScore)
    End Function

    ' =======================================================
    ' 6. EVENT HANDLER TOMBOL
    ' =======================================================

    ' Tombol Try Again (Button1)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Kembali ke Form2 untuk tes baru
        Me.Hide()
        Dim frmUtama As New Form2(_namaPengguna, _userId)
        frmUtama.Show()
    End Sub

    ' Tombol Exit (Button2) -> SEKARANG
    ' =======================================================
    ' NEW: RESPONSIVE CENTERING LOGIC
    ' =======================================================
    Private Sub Form3_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        CenterCard()
        Me.Invalidate()
    End Sub

    Private Sub CenterCard()
        If scrollPanel IsNot Nothing Then
            ' Center Panel2 horizontally and vertically
            scrollPanel.Left = (Me.ClientSize.Width - scrollPanel.Width) \ 2
            scrollPanel.Top = (Me.ClientSize.Height - scrollPanel.Height) \ 2 + 40
        End If

        ' Greeting Label5 tetap di kanan atas
        Label5.Left = Me.ClientSize.Width - Label5.Width - 30
        Label5.Top = 30
    End Sub

    ' Button History Click
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' NEW: Buka Form4 (History) dengan userId
        Dim frmHistory As New Form4(_userId)
        frmHistory.ShowDialog() ' Gunakan ShowDialog agar fokus ke history
    End Sub

    ' Tombol Play Recording (NEW: Diperlukan tombol di Designer)
    ' Saya asumsikan kita akan menambahkannya nanti atau menggunakan tombol baru
    Private Sub btnPlayMyVoice_Click(sender As Object, e As EventArgs) Handles btnPlayMyVoice.Click
        If String.IsNullOrEmpty(_userWavPath) OrElse Not System.IO.File.Exists(_userWavPath) Then
            MessageBox.Show("Recording file not found.", "Error")
            Return
        End If

        Try
            Using reader As New NAudio.Wave.WaveFileReader(_userWavPath)
                Using outputDevice As New NAudio.Wave.WaveOutEvent()
                    outputDevice.Init(reader)
                    outputDevice.Play()
                    While outputDevice.PlaybackState = NAudio.Wave.PlaybackState.Playing
                        Application.DoEvents()
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to play recording: {ex.Message}", "Playback Error")
        End Try
    End Sub
End Class