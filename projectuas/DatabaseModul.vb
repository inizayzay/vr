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
            Debug.WriteLine($"DB Connection FAILED: {ex.Message}")
            MessageBox.Show($"Database Connection Failed! Make sure XAMPP (MySQL) is running. Error: {ex.Message}",
                            "DB Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    ' FUNGSI MENGHITUNG XP DAN LEVEL USER
    Public Function GetUserXPAndLevel(ByVal userId As Integer) As Tuple(Of Integer, Double)
        Dim conn As MySqlConnection = Nothing
        Dim totalXP As Double = 0
        Dim level As Integer = 1

        Try
            conn = GetConnection()
            If conn Is Nothing Then Return Tuple.Create(1, 0.0)

            Dim sql As String = "SELECT SUM(final_score) as total FROM results WHERE user_id = @uid"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@uid", userId)

            Dim result = cmd.ExecuteScalar()
            If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                totalXP = Convert.ToDouble(result)
            End If

            ' Logic: 1000 XP (10 perfect tests) = 1 Level
            level = CInt(Math.Floor(totalXP / 1000)) + 1
            If level < 1 Then level = 1

        Catch ex As Exception
            Debug.WriteLine($"Error calculating XP: {ex.Message}")
        Finally
            CloseConnection(conn)
        End Try

        Return Tuple.Create(level, totalXP)
    End Function

End Module