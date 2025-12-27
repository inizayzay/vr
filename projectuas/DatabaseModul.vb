Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Data
Imports System.Diagnostics ' Diperlukan untuk Debug.WriteLine

' --- STRUKTUR DITEMPATKAN DI LUAR MODUL ---
Public Structure QuestionData
    Public Property ID As Integer
    Public Property Text As String
    Public Property Level As String
End Structure

Module DatabaseModule

    Private Const CONN_STRING As String = "server=localhost;user id=root;password=;database=db_vr"

    ' Fungsi untuk mendapatkan dan membuka koneksi
    Public Function GetConnection() As MySqlConnection
        Dim conn As MySqlConnection = Nothing
        Try
            conn = New MySqlConnection(CONN_STRING)
            conn.Open()
            Return conn
        Catch ex As Exception
            ' Menggunakan Debug.WriteLine untuk logging ke Output Window
            Debug.WriteLine($"Koneksi DB GAGAL: {ex.Message}")
            MessageBox.Show($"Koneksi Database Gagal! Pastikan XAMPP (MySQL) berjalan. Error: {ex.Message}",
                            "Koneksi DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ' Fungsi untuk menutup koneksi
    Public Sub CloseConnection(ByVal conn As MySqlConnection)
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
    End Sub

    ' FUNGSI MENGAMBIL PERTANYAAN ACAK
    Public Function GetRandomQuestion() As QuestionData
        Dim conn As MySqlConnection = Nothing
        Dim question As New QuestionData()

        ' Set ID ke 0 sebagai default aman
        question.ID = 0

        Try
            conn = GetConnection()
            If conn Is Nothing Then Return question

            Dim sql As String = "SELECT id, text, level FROM questions ORDER BY RAND() LIMIT 1"
            Dim cmd As New MySqlCommand(sql, conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                question.ID = reader.GetInt32("id")
                question.Text = reader.GetString("text").Trim()
                question.Level = reader.GetString("level").Trim()
            End If

            reader.Close()

        Catch ex As Exception
            ' Perbaikan: Menggunakan Debug.WriteLine untuk logging
            Debug.WriteLine($"Error retrieving random question: {ex.Message}")
            question.ID = -1
        Finally
            CloseConnection(conn)
        End Try

        Return question
    End Function

End Module