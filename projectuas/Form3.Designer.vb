<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Button1 = New Button()
        ImageList1 = New ImageList(components)
        Button2 = New Button()
        Panel1 = New Panel()
        Label5 = New Label()
        Label4 = New Label()
        Panel2 = New Panel()
        PictureBox1 = New PictureBox()
        Label6 = New Label()
        lblXP = New Label()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.WindowFrame
        Label1.Location = New Point(318, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(79, 25)
        Label1.TabIndex = 0
        Label1.Text = "RESULT"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 72F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label2.Location = New Point(252, 97)
        Label2.Name = "Label2"
        Label2.Size = New Size(219, 128)
        Label2.TabIndex = 1
        Label2.Text = "100"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label3.Location = New Point(309, 225)
        Label3.Name = "Label3"
        Label3.Size = New Size(115, 30)
        Label3.TabIndex = 2
        Label3.Text = "Good Job !"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.ForeColor = Color.White
        Button1.ImageAlign = ContentAlignment.MiddleLeft
        Button1.ImageIndex = 1
        Button1.ImageList = ImageList1
        Button1.Location = New Point(233, 297)
        Button1.Name = "Button1"
        Button1.Size = New Size(118, 23)
        Button1.TabIndex = 3
        Button1.Text = "Try Again "
        Button1.TextAlign = ContentAlignment.MiddleRight
        Button1.TextImageRelation = TextImageRelation.TextBeforeImage
        Button1.UseVisualStyleBackColor = False
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "clock.png")
        ImageList1.Images.SetKeyName(1, "refresh.png")
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Transparent
        Button2.FlatAppearance.BorderColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button2.FlatStyle = FlatStyle.Flat
        Button2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button2.ImageAlign = ContentAlignment.MiddleLeft
        Button2.ImageIndex = 0
        Button2.ImageList = ImageList1
        Button2.Location = New Point(386, 297)
        Button2.Name = "Button2"
        Button2.Size = New Size(97, 23)
        Button2.TabIndex = 4
        Button2.Text = "History"
        Button2.TextAlign = ContentAlignment.MiddleRight
        Button2.TextImageRelation = TextImageRelation.ImageBeforeText
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label4)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
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
        Label4.Location = New Point(27, 27)
        Label4.Name = "Label4"
        Label4.Size = New Size(253, 25)
        Label4.TabIndex = 5
        Label4.Text = "ENGLISH VOICE LEARNING"
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.BackColor = Color.White
        Panel2.Controls.Add(lblXP)
        Panel2.Controls.Add(Label6)
        Panel2.Controls.Add(PictureBox1)
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(Button2)
        Panel2.Controls.Add(Label3)
        Panel2.Location = New Point(91, 86)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(724, 364)
        Panel2.TabIndex = 6
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(309, 38)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(100, 83)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.DimGray
        Label6.Location = New Point(49, 255)
        Label6.Name = "Label6"
        Label6.Size = New Size(636, 23)
        Label6.TabIndex = 6
        Label6.Text = "Pronunciation is clear and perfect"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblXP
        ' 
        lblXP.AutoSize = True
        lblXP.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblXP.ForeColor = Color.DarkOrange
        lblXP.Location = New Point(318, 332)
        lblXP.Name = "lblXP"
        lblXP.Size = New Size(104, 21)
        lblXP.TabIndex = 7
        lblXP.Text = "Level 1 & 1 XP"
        ' 
        ' Form3
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        ClientSize = New Size(1008, 601)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "Form3"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Score Page Results"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents lblXP As Label
    Friend WithEvents Label6 As Label
End Class
