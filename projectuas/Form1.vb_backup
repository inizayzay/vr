Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class Form1

    ' Menggunakan nama toolbox default: TextBox1 (Nama Pengguna) dan Button1 (Mulai)

    ' =======================================================
    ' 1. FUNGSI UTAMA: GET OR CREATE USER ID
    ' =======================================================
    Private Function GetOrCreateUserId(ByVal nama As String) As Integer
        Dim conn As MySqlConnection = Nothing
        Dim userId As Integer = -1

        Try
            conn = DatabaseModule.GetConnection()
            If conn Is Nothing Then Return -1

            ' Cek apakah pengguna sudah ada
            Dim checkCmd As New MySqlCommand("SELECT id FROM users WHERE name = @name", conn)
            checkCmd.Parameters.AddWithValue("@name", nama)

            Dim result = checkCmd.ExecuteScalar()

            If result IsNot Nothing Then
                ' Pengguna ditemukan
                userId = CInt(result)
            Else
                ' Pengguna belum ada, masukkan pengguna baru
                Dim insertCmd As New MySqlCommand("INSERT INTO users (name, email) VALUES (@name, @email); SELECT LAST_INSERT_ID();", conn)

                insertCmd.Parameters.AddWithValue("@name", nama)
                insertCmd.Parameters.AddWithValue("@email", $"{nama.Replace(" ", "_")}@db_vr.com")

                userId = CInt(insertCmd.ExecuteScalar())
            End If

        Catch ex As Exception
            MessageBox.Show($"Database Error (Get/Create User): {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DatabaseModule.CloseConnection(conn)
        End Try

        Return userId
    End Function

    ' =======================================================
    ' 2. EVENT CLICK: TOMBOL MASUK (Button1)
    ' =======================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim namaPengguna As String = TextBox1.Text.Trim()

        If String.IsNullOrEmpty(namaPengguna) Then
            MessageBox.Show("Nama pengguna tidak boleh kosong.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim userId As Integer = GetOrCreateUserId(namaPengguna)

        If userId > 0 Then
            ' Sukses: Langsung menuju Form2 dan membawa userId
            Me.Hide()
            Dim frmUtama As New Form2(namaPengguna, userId)
            frmUtama.Show()
        Else
            MessageBox.Show("Gagal terhubung ke database atau membuat User ID.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' =======================================================
    ' 3. EVENT FORM LOAD
    ' =======================================================
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Halaman Input Nama"
        TextBox1.Focus()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class