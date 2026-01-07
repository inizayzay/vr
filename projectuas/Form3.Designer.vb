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
        scrollPanel = New Panel()
        btnPlayMyVoice = New Button()
        lblXP = New Label()
        Label6 = New Label()
        PictureBox1 = New PictureBox()
        Panel1.SuspendLayout()
        scrollPanel.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.WindowFrame
        Label1.Location = New Point(16, 13)
        Label1.Name = "Label1"
        Label1.Size = New Size(797, 32)
        Label1.TabIndex = 0
        Label1.Text = "RESULT"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Segoe UI", 52F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label2.Location = New Point(3, 151)
        Label2.Name = "Label2"
        Label2.Size = New Size(827, 109)
        Label2.TabIndex = 1
        Label2.Text = "100"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label3.Location = New Point(0, 260)
        Label3.Name = "Label3"
        Label3.Size = New Size(827, 40)
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
        Button1.ImageAlign = ContentAlignment.MiddleLeft
        Button1.ImageIndex = 1
        Button1.ImageList = ImageList1
        Button1.Location = New Point(266, 599)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(135, 31)
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
        Button2.Location = New Point(441, 599)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(111, 31)
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
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1152, 107)
        Panel1.TabIndex = 5
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.White
        Label5.Location = New Point(733, 49)
        Label5.Name = "Label5"
        Label5.Size = New Size(105, 32)
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
        ' scrollPanel
        ' 
        scrollPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        scrollPanel.BackColor = Color.White
        scrollPanel.Controls.Add(btnPlayMyVoice)
        scrollPanel.Controls.Add(lblXP)
        scrollPanel.Controls.Add(Label6)
        scrollPanel.Controls.Add(PictureBox1)
        scrollPanel.Controls.Add(Label1)
        scrollPanel.Controls.Add(Label2)
        scrollPanel.Controls.Add(Button1)
        scrollPanel.Controls.Add(Button2)
        scrollPanel.Controls.Add(Label3)
        scrollPanel.Location = New Point(104, 115)
        scrollPanel.Margin = New Padding(3, 4, 3, 4)
        scrollPanel.Name = "scrollPanel"
        scrollPanel.Size = New Size(827, 683)
        scrollPanel.TabIndex = 6
        ' 
        ' btnPlayMyVoice
        ' 
        btnPlayMyVoice.BackColor = Color.Ivory
        btnPlayMyVoice.FlatStyle = FlatStyle.Flat
        btnPlayMyVoice.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnPlayMyVoice.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        btnPlayMyVoice.Location = New Point(346, 553)
        btnPlayMyVoice.Name = "btnPlayMyVoice"
        btnPlayMyVoice.Size = New Size(140, 35)
        btnPlayMyVoice.TabIndex = 8
        btnPlayMyVoice.Text = "🔊 Play My Voice"
        btnPlayMyVoice.UseVisualStyleBackColor = False
        ' 
        ' lblXP
        ' 
        lblXP.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblXP.ForeColor = Color.DarkOrange
        lblXP.Location = New Point(0, 646)
        lblXP.Name = "lblXP"
        lblXP.Size = New Size(827, 37)
        lblXP.TabIndex = 7
        lblXP.Text = "Level 1 & 1 XP"
        lblXP.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.DimGray
        Label6.Location = New Point(0, 300)
        Label6.Name = "Label6"
        Label6.Size = New Size(827, 250)
        Label6.TabIndex = 6
        Label6.Text = "Evaluating in process.."
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(353, 51)
        PictureBox1.Margin = New Padding(3, 4, 3, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(114, 111)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' Form3
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        ClientSize = New Size(1152, 801)
        Controls.Add(scrollPanel)
        Controls.Add(Panel1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form3"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Score Page Results"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        scrollPanel.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents scrollPanel As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents lblXP As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnPlayMyVoice As Button
End Class
