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

    ' --- Deklarasi Komponen (Asumsi default names) ---
    ' Label1 = Your score (Kita akan gunakan ini untuk Total Kumulatif)
    ' Label2 = 100 (Kita akan gunakan ini untuk Skor Tes SAAT INI)
    ' Label3 = Good Job !
    ' Button1 = Try Again
    ' Button2 = Exit

    ' =======================================================
    ' 1. CONSTRUCTOR
    ' =======================================================
    Public Sub New(ByVal nama As String, ByVal target As String, ByVal diucapkan As String, ByVal userId As Integer, ByVal questionId As Integer)
        InitializeComponent()
        _namaPengguna = nama
        _teksTarget = target
        _teksDiucapkan = diucapkan
        _userId = userId
        _questionId = questionId
    End Sub

    ' =======================================================
    ' 2. FORM LOAD
    ' =======================================================
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Hasil Tes Pengucapan"
        Me.StartPosition = FormStartPosition.CenterScreen

        ' 1. HITUNG SKOR (Word Accuracy)
        _scorePersen = HitungAkurasi(_teksTarget, _teksDiucapkan)

        Dim recognizedTextDisplay As String = _teksDiucapkan
        Dim displayColor As Color = Color.White

        ' Cek jika ada pesan error dari Form2 
        If _teksDiucapkan.StartsWith("[") Then
            _scorePersen = 0.0 ' Jika ada pesan spesial (ERROR, PENGECUALIAN), skor 0
            recognizedTextDisplay = "ERROR/PENGECUALIAN: " & _teksDiucapkan
            displayColor = Color.LightPink
        End If

        ' 2. SIMPAN HASIL KE DATABASE
        If _userId > 0 AndAlso Not _teksDiucapkan.StartsWith("[") Then
            SaveResultToDatabase(_userId, _questionId, _scorePersen)
        End If

        ' 3. TAMPILKAN SKOR TES SAAT INI (Label2)
        Label2.Text = $"{_scorePersen:N2}%"
        Label2.ForeColor = If(_scorePersen >= 80, Color.Green, Color.Red)

        ' 4. TAMPILKAN PESAN FEEDBACK (Label3)
        If _scorePersen = 100.0 Then
            Label3.Text = "Perfect Score! Good Job!"
        ElseIf _scorePersen >= 80.0 Then
            Label3.Text = "Good Job! You are improving."
        Else
            Label3.Text = "Keep Practicing! Try Again."
        End If

        ' 5. TAMPILKAN TOTAL SKOR KUMULATIF (Label1)
        Dim totalScoreText As String = GetCumulativeScore(_userId)
        Label1.Text = $"Your total score: {totalScoreText}" ' Total skor di sini (Label1)
    End Sub

    ' =======================================================
    ' 3. FUNGSI SIMPAN HASIL KE DATABASE (Tabel: results)
    ' =======================================================
    Private Sub SaveResultToDatabase(ByVal userId As Integer, ByVal questionId As Integer, ByVal score As Double)
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseModule.GetConnection()
            If conn Is Nothing Then Exit Sub

            Dim sql As String = "INSERT INTO results (user_id, question_id, word_accuracy, phoneme_accuracy, final_score) " &
                                "VALUES (@uid, @qid, @wordAcc, @phoneAcc, @finalScore)"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@uid", userId)
            cmd.Parameters.AddWithValue("@qid", questionId)
            cmd.Parameters.AddWithValue("@wordAcc", score)
            cmd.Parameters.AddWithValue("@phoneAcc", 0.0) ' Set 0.0 karena kita hanya menghitung Word Accuracy
            cmd.Parameters.AddWithValue("@finalScore", score)

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
    ' 5. FUNGSI PERHITUNGAN AKURASI (Word Intersection)
    ' (Harus sama dengan yang Anda gunakan di project Anda)
    ' =======================================================
    Private Function HitungAkurasi(ByVal target As String, ByVal actual As String) As Double
        ' 1. Fungsi pembersihan string
        Dim CleanString = Function(s As String) As String
                              ' Hapus tanda baca dan ubah ke lowercase
                              Return New String(s.Where(Function(c) Char.IsLetterOrDigit(c) OrElse Char.IsWhiteSpace(c)).ToArray()).ToLower()
                          End Function

        ' 2. Tokenisasi (Pemisahan kata)
        Dim targetWords As String() = CleanString(target).Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim actualWords As String() = CleanString(actual).Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        Dim targetLength As Integer = targetWords.Length

        If targetLength = 0 Then Return 0

        ' 3. Gunakan HashSet untuk menghitung kata-kata yang cocok (Word Intersection)
        Dim targetSet As New HashSet(Of String)(targetWords)
        Dim correctCount As Integer = 0

        ' 4. Iterasi kata yang diucapkan dan bandingkan dengan Set Target
        For Each word In actualWords
            If targetSet.Contains(word) Then
                correctCount += 1
                ' Hapus kata yang sudah cocok dari targetSet untuk menghindari penghitungan ganda
                targetSet.Remove(word)
            End If
        Next

        ' 5. Hitung skor
        Dim finalScore As Double = (CDbl(correctCount) / targetLength) * 100

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

    ' Tombol Exit (Button2)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub
End Class