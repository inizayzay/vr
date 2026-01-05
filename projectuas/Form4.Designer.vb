<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
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
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form4))
        DataGridView1 = New DataGridView()
        colNo = New DataGridViewTextBoxColumn()
        colTanggal = New DataGridViewTextBoxColumn()
        colNama = New DataGridViewTextBoxColumn()
        colEmail = New DataGridViewTextBoxColumn()
        colSkor = New DataGridViewTextBoxColumn()
        Button1 = New Button()
        ImageList1 = New ImageList(components)
        Panel1 = New Panel()
        Label1 = New Label()
        Panel2 = New Panel()
        picChart = New PictureBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(picChart, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.BackgroundColor = Color.White
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {colNo, colTanggal, colNama, colEmail, colSkor})
        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.Location = New Point(0, 148)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersVisible = False
        DataGridView1.Size = New Size(680, 172)
        DataGridView1.TabIndex = 0
        ' 
        ' colNo
        ' 
        colNo.HeaderText = "No"
        colNo.Name = "colNo"
        ' 
        ' colTanggal
        ' 
        colTanggal.HeaderText = "Tanggal"
        colTanggal.Name = "colTanggal"
        ' 
        ' colNama
        ' 
        colNama.HeaderText = "Nama User"
        colNama.Name = "colNama"
        ' 
        ' colEmail
        ' 
        colEmail.HeaderText = "Email"
        colEmail.Name = "colEmail"
        ' 
        ' colSkor
        ' 
        colSkor.HeaderText = "Skor"
        colSkor.Name = "colSkor"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.Transparent
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Button1.ImageIndex = 0
        Button1.ImageList = ImageList1
        Button1.Location = New Point(22, 56)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 1
        Button1.Text = "Back"
        Button1.TextImageRelation = TextImageRelation.ImageBeforeText
        Button1.UseVisualStyleBackColor = False
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "arrow-left.png")
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.Controls.Add(picChart)
        Panel1.Controls.Add(Button1)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(DataGridView1)
        Panel1.Location = New Point(115, 63)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(680, 327)
        Panel1.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label1.Location = New Point(270, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(115, 32)
        Label1.TabIndex = 3
        Label1.Text = "HISTORY"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(50), CByte(40), CByte(100))
        Panel2.Location = New Point(110, 59)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(697, 341)
        Panel2.TabIndex = 3
        ' 
        ' picChart
        ' 
        picChart.Location = New Point(-5, 95)
        picChart.Name = "picChart"
        picChart.Size = New Size(682, 50)
        picChart.TabIndex = 4
        picChart.TabStop = False
        ' 
        ' Form4
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        ClientSize = New Size(1008, 601)
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Name = "Form4"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Halaman History"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(picChart, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents colNo As DataGridViewTextBoxColumn
    Friend WithEvents colTanggal As DataGridViewTextBoxColumn
    Friend WithEvents colNama As DataGridViewTextBoxColumn
    Friend WithEvents colEmail As DataGridViewTextBoxColumn
    Friend WithEvents colSkor As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents picChart As PictureBox
End Class
