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
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class