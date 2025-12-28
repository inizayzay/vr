<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label2 = New Label()
        btnListen = New Button()
        Button2 = New Button()
        Panel1 = New Panel()
        btnHistory = New Button()
        Label3 = New Label()
        Label1 = New Label()
        Button1 = New Button()
        Panel2 = New Panel()
        Panel3 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(108), CByte(98), CByte(231))
        Label2.Location = New Point(87, 57)
        Label2.Name = "Label2"
        Label2.Size = New Size(572, 62)
        Label2.TabIndex = 1
        Label2.Text = "WELCOME TO TES VOICE"
        ' 
        ' btnListen
        ' 
        btnListen.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        btnListen.FlatAppearance.BorderSize = 0
        btnListen.FlatStyle = FlatStyle.Flat
        btnListen.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnListen.ForeColor = Color.White
        btnListen.Location = New Point(320, 120)
        btnListen.Name = "btnListen"
        btnListen.Size = New Size(80, 30)
        btnListen.TabIndex = 4
        btnListen.Text = "🔊 Listen"
        btnListen.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.GreenYellow
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button2.Location = New Point(606, 299)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(86, 39)
        Button2.TabIndex = 3
        Button2.Text = "Next"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' btnHistory
        ' 
        btnHistory.BackColor = Color.FromArgb(CByte(50), CByte(40), CByte(100))
        btnHistory.FlatAppearance.BorderSize = 0
        btnHistory.FlatStyle = FlatStyle.Flat
        btnHistory.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnHistory.ForeColor = Color.White
        btnHistory.Location = New Point(200, 35)
        btnHistory.Name = "btnHistory"
        btnHistory.Size = New Size(100, 32)
        btnHistory.TabIndex = 2
        btnHistory.Text = "📜 History"
        btnHistory.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Panel1.Controls.Add(btnHistory)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1355, 103)
        Panel1.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.White
        Label3.Location = New Point(809, 67)
        Label3.Name = "Label3"
        Label3.Size = New Size(86, 28)
        Label3.TabIndex = 1
        Label3.Text = "HI, User"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(14, 35)
        Label1.Name = "Label1"
        Label1.Size = New Size(173, 32)
        Label1.TabIndex = 0
        Label1.Text = "Voice Practice"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.White
        Button1.Location = New Point(202, 167)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(286, 80)
        Button1.TabIndex = 2
        Button1.Text = "🎤 Tap to Speak"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.BackColor = Color.White
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(btnListen)
        Panel2.Controls.Add(Button2)
        Panel2.Location = New Point(3, 4)
        Panel2.Margin = New Padding(3, 4, 3, 4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(721, 430)
        Panel2.TabIndex = 5
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(50), CByte(40), CByte(100))
        Panel3.Controls.Add(Panel2)
        Panel3.Location = New Point(324, 147)
        Panel3.Margin = New Padding(3, 4, 3, 4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(736, 415)
        Panel3.TabIndex = 4
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(98), CByte(231))
        ClientSize = New Size(1355, 724)
        Controls.Add(Panel3)
        Controls.Add(Panel1)
        Cursor = Cursors.Hand
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form2"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Halaman Tes Voice"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents btnListen As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnHistory As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
End Class
