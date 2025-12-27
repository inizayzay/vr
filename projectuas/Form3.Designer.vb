<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Button1 = New Button()
        Button2 = New Button()

        Label6 = New Label()
        Panel1 = New Panel()
        Label5 = New Label()
        Label4 = New Label()
        Panel2 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = False
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.WindowFrame
        Label1.Location = New Point(300, 16)
        Label1.Name = "Label1"
        Label1.Size = New Size(666, 32)
        Label1.TabIndex = 0
        Label1.Text = "RESULT"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = False
        Label2.Font = New Font("Segoe UI", 72F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label2.Location = New Point(233, 41)
        Label2.Name = "Label2"
        Label2.Size = New Size(666, 159)
        Label2.TabIndex = 1
        Label2.Text = "100"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.AutoSize = False
        Label3.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label3.Location = New Point(278, 162)
        Label3.Name = "Label3"
        Label3.Size = New Size(666, 40)
        Label3.TabIndex = 2
        Label3.Text = "Good Job !"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.ForeColor = Color.White
        Button1.Location = New Point(277, 214)
        Button1.Name = "Button1"
        Button1.Size = New Size(135, 31)
        Button1.TabIndex = 3
        Button1.Text = "Try Again ↺"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Transparent
        Button2.FlatAppearance.BorderColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button2.FlatStyle = FlatStyle.Flat
        Button2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button2.Location = New Point(280, 255)
        Button2.Name = "Button2"
        Button2.Size = New Size(135, 31)
        Button2.TabIndex = 4
        Button2.Text = "History"
        Button2.UseVisualStyleBackColor = False
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Label6
        ' 
        Label6.AutoSize = False
        Label6.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.Black
        Label6.Location = New Point(0, 270)
        Label6.Name = "Label6"
        Label6.Size = New Size(666, 70)
        Label6.TabIndex = 7
        Label6.Text = "Detail: ..."
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label4)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1008, 80)
        Panel1.TabIndex = 5
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.White
        Label5.Location = New Point(641, 37)
        Label5.Name = "Label5"
        Label5.Size = New Size(83, 25)
        Label5.TabIndex = 6
        Label5.Text = "HI, User"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.White
        Label4.Location = New Point(31, 36)
        Label4.Name = "Label4"
        Label4.Size = New Size(319, 32)
        Label4.TabIndex = 5
        Label4.Text = "ENGLISH VOICE LEARNING"
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.BackColor = Color.White
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(Button2)
        Panel2.Controls.Add(Label3)
        Panel2.Location = New Point(27, 86)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(724, 364)
        Panel2.TabIndex = 6
        ' 
        ' Form3
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        ClientSize = New Size(1008, 601)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form3"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Score Page Results"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
End Class
