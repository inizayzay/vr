Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq
Imports MySql.Data.MySqlClient ' Diperlukan untuk interaksi DB

Public Class Form3
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
    Private _dtwScore As Double     ' DTW similarity score (0-100)
    Private _combinedScore As Double ' Combined score (DTW + Word accuracy)

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
    ' 2. FORM LOAD
    ' =======================================================
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Hasil Tes Pengucapan"
        Me.StartPosition = FormStartPosition.CenterScreen

        ' 1. HITUNG SKOR WORD ACCURACY
        _scorePersen = HitungAkurasi(_teksTarget, _teksDiucapkan)

        Dim recognizedTextDisplay As String = _teksDiucapkan
        Dim displayColor As Color = Color.White

        ' Cek jika ada pesan error dari Form2 
        If _teksDiucapkan.StartsWith("[") Then
            _scorePersen = 0.0 ' Jika ada pesan spesial (ERROR, PENGECUALIAN), skor 0
            recognizedTextDisplay = "ERROR/PENGECUALIAN: " & _teksDiucapkan
            displayColor = Color.LightPink
        End If
        
        ' 2. NEW: HITUNG DTW SIMILARITY SCORE
        _dtwScore = CalculateDTWSimilarity()
        
        ' 3. NEW: HITUNG COMBINED SCORE (70% DTW + 30% Word Accuracy)
        _combinedScore = (_dtwScore * 0.7) + (_scorePersen * 0.3)

        ' 4. SIMPAN HASIL KE DATABASE
        If _userId > 0 AndAlso Not _teksDiucapkan.StartsWith("[") Then
            SaveResultToDatabase(_userId, _questionId, _combinedScore, _dtwScore, _scorePersen)
        End If

        ' 5. TAMPILKAN SKOR COMBINED (Label2)
        Label2.Text = $"{_combinedScore:N2}%"
        Label2.ForeColor = If(_combinedScore >= 80, Color.Green, Color.Red)

        ' 6. TAMPILKAN PESAN FEEDBACK (Label3)
        If _combinedScore = 100.0 Then
            Label3.Text = "Perfect Score! Good Job!"
        ElseIf _combinedScore >= 80.0 Then
            Label3.Text = $"Good Job! (DTW: {_dtwScore:N1}%, Word: {_scorePersen:N1}%)"
        Else
            Label3.Text = $"Keep Practicing! (DTW: {_dtwScore:N1}%, Word: {_scorePersen:N1}%)"
        End If

        ' 7. TAMPILKAN TOTAL SKOR KUMULATIF (Label1)
        Dim totalScoreText As String = GetCumulativeScore(_userId)
        Label1.Text = $"Your total score: {totalScoreText}" ' Total skor di sini (Label1)

        ' 8. TAMPILKAN DETAIL UCAPAN (Label6)
        Label6.Text = $"Target: {_teksTarget}{vbCrLf}You said: {_teksDiucapkan}"

        ' 9. UPDATE HEADER GREETING (Label5)
        Label5.Text = $"HI, {_namaPengguna}"
    End Sub

    ' =======================================================
    ' 3. FUNGSI SIMPAN HASIL KE DATABASE (Tabel: results)
    ' =======================================================
    Private Sub SaveResultToDatabase(ByVal userId As Integer, ByVal questionId As Integer, 
                                      ByVal finalScore As Double, ByVal dtwScore As Double, 
                                      ByVal wordAccuracy As Double)
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseModule.GetConnection()
            If conn Is Nothing Then Exit Sub

            Dim sql As String = "INSERT INTO results (user_id, question_id, word_accuracy, phoneme_accuracy, final_score, dtw_score, audio_file_path) " &
                                "VALUES (@uid, @qid, @wordAcc, @phoneAcc, @finalScore, @dtwScore, @audioPath)"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@uid", userId)
            cmd.Parameters.AddWithValue("@qid", questionId)
            cmd.Parameters.AddWithValue("@wordAcc", wordAccuracy)
            cmd.Parameters.AddWithValue("@phoneAcc", 0.0) ' Placeholder
            cmd.Parameters.AddWithValue("@finalScore", finalScore)
            cmd.Parameters.AddWithValue("@dtwScore", dtwScore)
            cmd.Parameters.AddWithValue("@audioPath", _userWavPath)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show($"Gagal menyimpan hasil ke database: {ex.Message}", "DB Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    ' NEW: CALCULATE DTW SIMILARITY
    ' =======================================================
    Private Function CalculateDTWSimilarity() As Double
        Try
            ' Check if audio file exists
            Debug.WriteLine("=== DTW Calculation Start ===")
            Debug.WriteLine($"User WAV Path: {_userWavPath}")
            
            If String.IsNullOrEmpty(_userWavPath) OrElse Not System.IO.File.Exists(_userWavPath) Then
                Debug.WriteLine("ERROR: No audio file recorded or file not found")
                MessageBox.Show("Audio file tidak ditemukan. DTW score akan 0.", "Audio Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return 0.0
            End If
            
            Debug.WriteLine("Audio file exists, extracting MFCC...")
            
            ' 1. Extract MFCC from user audio
            Dim userMFCC As Double(,) = MFCCExtractor.ExtractMFCC(_userWavPath)
            Debug.WriteLine($"User MFCC extracted: {userMFCC.GetLength(0)} x {userMFCC.GetLength(1)}")
            
            ' 2. Load reference MFCC
            ' Replace spaces with underscores and trim/lowercase (matching fetch_and_generate.py)
            Dim safeTargetName As String = _teksTarget.Trim().ToLower().Replace(" ", "_")
            Dim refPath As String = System.IO.Path.Combine(Application.StartupPath, "references", $"{safeTargetName}_mfcc.json")
            
            Debug.WriteLine($"Reference path: {refPath}")
            Debug.WriteLine($"File exists: {System.IO.File.Exists(refPath)}")
            
            If Not System.IO.File.Exists(refPath) Then
                Debug.WriteLine($"ERROR: Reference MFCC not found at: {refPath}")
                
                ' Diagnostics: Check directory content
                Dim refDir As String = System.IO.Path.Combine(Application.StartupPath, "references")
                If System.IO.Directory.Exists(refDir) Then
                    Dim files = System.IO.Directory.GetFiles(refDir)
                    Debug.WriteLine($"Files in references folder ({files.Length}):")
                    For Each f In files
                        Debug.WriteLine($"  - {System.IO.Path.GetFileName(f)}")
                    Next
                Else
                    Debug.WriteLine($"CRITICAL: Directory '{refDir}' does not exist!")
                End If

                MessageBox.Show($"Reference MFCC tidak ditemukan untuk '{_teksTarget}'." & vbCrLf &
                               $"Teks Target: {_teksTarget}" & vbCrLf &
                               $"Mencari file: {safeTargetName}_mfcc.json" & vbCrLf &
                               $"Path: {refPath}" & vbCrLf & vbCrLf &
                               "Pastikan file sudah ada di folder 'references' dan properti 'Copy to Output Directory' sudah diset ke 'Copy if newer'.", 
                               "Reference Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return 0.0
            End If
            
            Debug.WriteLine("Loading reference MFCC...")
            Dim refMFCC As Double(,) = MFCCExtractor.LoadReferenceFromJson(refPath)
            Debug.WriteLine($"Reference MFCC loaded: {refMFCC.GetLength(0)} x {refMFCC.GetLength(1)}")
            
            ' 3. Calculate DTW distance
            Debug.WriteLine("Calculating DTW distance...")
            Dim distance As Double = DTWComparator.CalculateDTWDistance(userMFCC, refMFCC)
            Debug.WriteLine($"DTW Distance: {distance:F4}")
            
            ' 4. Convert to similarity score (0-100)
            Dim similarity As Double = DTWComparator.DistanceToSimilarity(distance)
            Debug.WriteLine($"DTW Similarity: {similarity:F2}%")
            Debug.WriteLine("=== DTW Calculation End ===")
            
            Return similarity
            
        Catch ex As Exception
            Debug.WriteLine($"EXCEPTION in CalculateDTWSimilarity: {ex.Message}")
            Debug.WriteLine($"Stack trace: {ex.StackTrace}")
            MessageBox.Show($"Error menghitung DTW similarity:{vbCrLf}{ex.Message}", "DTW Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0.0
        End Try
    End Function

    ' =======================================================
    ' 5. FUNGSI PERHITUNGAN AKURASI (Word Intersection)
    ' (Harus sama dengan yang Anda gunakan di project Anda)
    ' =======================================================
    Private Function HitungAkurasi(ByVal target As String, ByVal actual As String) As Double
        ' DEBUG: Log input values
        Debug.WriteLine("=== Word Accuracy Calculation ===")
        Debug.WriteLine($"Target: '{target}'")
        Debug.WriteLine($"Actual: '{actual}'")

        ' 1. Fungsi pembersihan string
        Dim CleanString = Function(s As String) As String
                              ' Hapus tanda baca dan ubah ke lowercase
                              Return New String(s.Where(Function(c) Char.IsLetterOrDigit(c) OrElse Char.IsWhiteSpace(c)).ToArray()).ToLower()
                          End Function

        ' 2. Tokenisasi (Pemisahan kata)
        Dim targetWords As String() = CleanString(target).Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim actualWords As String() = CleanString(actual).Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        ' DEBUG: Log cleaned words
        Debug.WriteLine($"Target words: [{String.Join(", ", targetWords)}]")
        Debug.WriteLine($"Actual words: [{String.Join(", ", actualWords)}]")

        Dim targetLength As Integer = targetWords.Length

        If targetLength = 0 Then
            Debug.WriteLine("Target length is 0, returning 0")
            Return 0
        End If

        ' 3. Gunakan HashSet untuk menghitung kata-kata yang cocok (Word Intersection)
        Dim targetSet As New HashSet(Of String)(targetWords)
        Dim correctCount As Integer = 0

        ' 4. Iterasi kata yang diucapkan dan bandingkan dengan Set Target
        For Each word In actualWords
            If targetSet.Contains(word) Then
                correctCount += 1
                Debug.WriteLine($"Match found: '{word}'")
                ' Hapus kata yang sudah cocok dari targetSet untuk menghindari penghitungan ganda
                targetSet.Remove(word)
            Else
                Debug.WriteLine($"No match: '{word}'")
            End If
        Next

        ' 5. Hitung skor
        Dim finalScore As Double = (CDbl(correctCount) / targetLength) * 100

        Debug.WriteLine($"Correct count: {correctCount} / {targetLength}")
        Debug.WriteLine($"Final score: {finalScore:N2}%")
        Debug.WriteLine("=================================")

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

    ' Tombol Exit (Button2) -> SEKARANG JADI Tombol History
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' NEW: Buka Form4 (History) dengan userId
        Dim frmHistory As New Form4(_userId)
        frmHistory.ShowDialog() ' Gunakan ShowDialog agar fokus ke history
    End Sub
End Class