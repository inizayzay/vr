Imports MySql.Data.MySqlClient

Public Class Form4
    ' =======================================================
    ' VARIABEL UTAMA
    ' =======================================================
    Private _userId As Integer

    ' =======================================================
    ' CONSTRUCTOR
    ' =======================================================
    Public Sub New(ByVal userId As Integer)
        InitializeComponent()
        _userId = userId
    End Sub

    ' =======================================================
    ' 1. FORM LOAD: AMBIL DATA HISTORY
    ' =======================================================
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadHistoryData()
    End Sub

    Private Sub LoadHistoryData()
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseModule.GetConnection()
            If conn Is Nothing Then Exit Sub

            ' Query Join antara table results (skor) dan users (nama/email)
            ' Filter hanya untuk user yang sedang login
            ' Urutkan berdasarkan waktu terbaru (asumsi id increment)
            Dim sql As String = "SELECT r.created_at, u.name, u.email, r.final_score " &
                                "FROM results r " &
                                "JOIN users u ON r.user_id = u.id " &
                                "WHERE r.user_id = @userId " &
                                "ORDER BY r.id DESC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@userId", _userId)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            DataGridView1.Rows.Clear()

            Dim no As Integer = 1
            While reader.Read()
                Dim tgl As String = reader.GetDateTime("created_at").ToString("yyyy-MM-dd HH:mm")
                Dim nama As String = reader.GetString("name")
                Dim email As String = reader.GetString("email")
                Dim skor As Double = reader.GetDouble("final_score")

                DataGridView1.Rows.Add(no, tgl, nama, email, $"{skor:N2}%")
                no += 1
            End While

            reader.Close()

        Catch ex As Exception
            MessageBox.Show($"Gagal memuat data history: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DatabaseModule.CloseConnection(conn)
        End Try
    End Sub

    ' =======================================================
    ' 2. TOMBOL BACK (Button1)
    ' =======================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class