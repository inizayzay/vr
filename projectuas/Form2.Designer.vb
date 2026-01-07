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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Label2 = New Label()
        Button2 = New Button()
        ImageList1 = New ImageList(components)
        Panel1 = New Panel()
        PictureBox1 = New PictureBox()
        Label3 = New Label()
        Label1 = New Label()
        Button1 = New Button()
        Panel2 = New Panel()
        picWaveform = New PictureBox()
        btnListen = New Button()
        btnHistory = New Button()
        Label5 = New Label()
        Panel3 = New Panel()
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        CType(picWaveform, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(108), CByte(98), CByte(231))
        Label2.Location = New Point(0, 48)
        Label2.Name = "Label2"
        Label2.Size = New Size(903, 62)
        Label2.TabIndex = 1
        Label2.Text = "READ THE SENTENCE BELOW"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Transparent
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button2.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button2.ImageAlign = ContentAlignment.MiddleLeft
        Button2.ImageIndex = 0
        Button2.ImageList = ImageList1
        Button2.Location = New Point(718, 283)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Padding = New Padding(11, 0, 11, 0)
        Button2.Size = New Size(117, 39)
        Button2.TabIndex = 3
        Button2.Text = "Next"
        Button2.TextAlign = ContentAlignment.MiddleRight
        Button2.TextImageRelation = TextImageRelation.TextBeforeImage
        Button2.UseVisualStyleBackColor = False
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "arrow-right.png")
        ImageList1.Images.SetKeyName(1, "list.png")
        ImageList1.Images.SetKeyName(2, "clock.png")
        ImageList1.Images.SetKeyName(3, "microphone.png")
        ImageList1.Images.SetKeyName(4, "arrow-left.png")
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1152, 107)
        Panel1.TabIndex = 4
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(757, 55)
        PictureBox1.Margin = New Padding(3, 4, 3, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(56, 40)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
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
        Button1.Location = New Point(308, 153)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(286, 80)
        Button1.TabIndex = 2
        Button1.Text = "🎤 "
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.BackColor = Color.White
        Panel2.Controls.Add(picWaveform)
        Panel2.Controls.Add(btnListen)
        Panel2.Controls.Add(btnHistory)
        Panel2.Controls.Add(Label5)
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Button2)
        Panel2.Location = New Point(3, 4)
        Panel2.Margin = New Padding(3, 4, 3, 4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(903, 396)
        Panel2.TabIndex = 5
        ' 
        ' picWaveform
        ' 
        picWaveform.BackColor = Color.Black
        picWaveform.Dock = DockStyle.Bottom
        picWaveform.Location = New Point(0, 329)
        picWaveform.Margin = New Padding(3, 4, 3, 4)
        picWaveform.Name = "picWaveform"
        picWaveform.Size = New Size(903, 67)
        picWaveform.TabIndex = 7
        picWaveform.TabStop = False
        ' 
        ' btnListen
        ' 
        btnListen.Location = New Point(769, 124)
        btnListen.Margin = New Padding(3, 4, 3, 4)
        btnListen.Name = "btnListen"
        btnListen.Size = New Size(86, 47)
        btnListen.TabIndex = 6
        btnListen.Text = "🔊"
        btnListen.UseVisualStyleBackColor = True
        ' 
        ' btnHistory
        ' 
        btnHistory.FlatStyle = FlatStyle.Flat
        btnHistory.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        btnHistory.Location = New Point(769, 33)
        btnHistory.Margin = New Padding(3, 4, 3, 4)
        btnHistory.Name = "btnHistory"
        btnHistory.Size = New Size(86, 31)
        btnHistory.TabIndex = 5
        btnHistory.Text = "History"
        btnHistory.UseVisualStyleBackColor = True
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(50), CByte(40), CByte(100))
        Panel3.Controls.Add(Panel2)
        Panel3.Location = New Point(88, 111)
        Panel3.Margin = New Padding(3, 4, 3, 4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(918, 415)
        Panel3.TabIndex = 4
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(98), CByte(231))
        ClientSize = New Size(1152, 775)
        Controls.Add(Panel3)
        Controls.Add(Panel1)
        Cursor = Cursors.Hand
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form2"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Voice Test Page"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(picWaveform, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label5 As Label
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnListen As Button
    Friend WithEvents picWaveform As PictureBox
End Class
