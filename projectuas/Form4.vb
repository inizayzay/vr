Imports MySql.Data.MySqlClient

Public Class Form4
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
    Private _userId As Integer
    Private _historyScores As New List(Of Double)

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
        ' Tampilkan XP dan Level
        Dim xpData = DatabaseModule.GetUserXPAndLevel(_userId)
        lblXP.Text = $"Level {xpData.Item1} ({xpData.Item2:N0} XP)"
        
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
            Dim sql As String = "SELECT r.created_at, u.name, u.email, r.spoken_text, r.final_score " &
                                "FROM results r " &
                                "JOIN users u ON r.user_id = u.id " &
                                "WHERE r.user_id = @userId " &
                                "ORDER BY r.id DESC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@userId", _userId)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            DataGridView1.Rows.Clear()
            _historyScores.Clear()

            Dim no As Integer = 1
            While reader.Read()
                Dim tgl As String = reader.GetDateTime("created_at").ToString("yyyy-MM-dd HH:mm")
                Dim nama As String = reader.GetString("name")
                Dim email As String = reader.GetString("email")
                
                ' Ambil spoken_text, tangani NULL jika ada data lama
                Dim ucapan As String = "-"
                If Not reader.IsDBNull(reader.GetOrdinal("spoken_text")) Then
                    ucapan = reader.GetString("spoken_text")
                End If

                Dim skor As Double = reader.GetDouble("final_score")
                _historyScores.Add(skor)

                ' Tambahkan baris sesuai urutan kolom baru {No, Tanggal, Nama, Email, Skor}
                DataGridView1.Rows.Add(no, tgl, nama, email, $"{skor:N2}%")
                no += 1
            End While

            reader.Close()
            
            ' Draw Chart
            DrawProgressChart()

        Catch ex As Exception
            MessageBox.Show($"Failed to load history data: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DatabaseModule.CloseConnection(conn)
        End Try
    End Sub

    ' =======================================================
    ' 3. CHART DRAWING LOGIC
    ' =======================================================
    Private Sub DrawProgressChart()
        If _historyScores.Count < 1 Then Return
        picChart.Invalidate()
    End Sub

    Private Sub picChart_Paint(sender As Object, e As PaintEventArgs) Handles picChart.Paint
        If _historyScores.Count < 1 Then Return

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim w As Integer = picChart.Width
        Dim h As Integer = picChart.Height
        Dim margin As Integer = 30
        
        ' Draw Axes
        Dim axisPen As New Pen(Color.Gray, 1)
        g.DrawLine(axisPen, margin, margin, margin, h - margin) ' Y
        g.DrawLine(axisPen, margin, h - margin, w - margin, h - margin) ' X

        ' Draw Grid & Labels
        Dim font As New Font("Segoe UI", 8)
        Dim textBrush As New SolidBrush(Color.Gray)
        g.DrawString("100%", font, textBrush, 0, margin)
        g.DrawString("0%", font, textBrush, 5, h - margin - 10)

        ' Process Data (Reverse to show oldest to newest left-to-right)
        Dim displayScores = _historyScores.AsEnumerable().Reverse().ToList()
        If displayScores.Count < 1 Then Return

        Dim xStep As Single = (w - 2 * margin) / Math.Max(displayScores.Count - 1, 1)
        Dim yScale As Single = (h - 2 * margin) / 100.0F

        Dim points As New List(Of PointF)
        For i As Integer = 0 To displayScores.Count - 1
            Dim x As Single = margin + (i * xStep)
            Dim y As Single = (h - margin) - (displayScores(i) * yScale)
            points.Add(New PointF(x, y))
        Next

        ' Draw Line
        If points.Count > 1 Then
            Dim linePen As New Pen(Color.FromArgb(108, 92, 231), 3)
            g.DrawLines(linePen, points.ToArray())
        End If

        ' Draw Points and Tooltips/Values
        For Each p In points
            g.FillEllipse(Brushes.IndianRed, p.X - 4, p.Y - 4, 8, 8)
        Next
    End Sub

    ' =======================================================
    ' 4. TOMBOL BACK (Button1)
    ' =======================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class