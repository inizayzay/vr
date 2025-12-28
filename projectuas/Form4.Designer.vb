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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataGridView1 = New DataGridView()
        colNo = New DataGridViewTextBoxColumn()
        colTanggal = New DataGridViewTextBoxColumn()
        colNama = New DataGridViewTextBoxColumn()
        colEmail = New DataGridViewTextBoxColumn()
        colUcapan = New DataGridViewTextBoxColumn()
        colSkor = New DataGridViewTextBoxColumn()
        Button1 = New Button()
        Panel1 = New Panel()
        Label1 = New Label()
        Panel2 = New Panel()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.BackgroundColor = Color.White
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {colNo, colTanggal, colNama, colEmail, colUcapan, colSkor})
        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.Location = New Point(0, 113)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersVisible = False
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(1066, 422)
        DataGridView1.TabIndex = 0
        ' 
        ' colNo
        ' 
        colNo.HeaderText = "No"
        colNo.MinimumWidth = 6
        colNo.Name = "colNo"
        ' 
        ' colTanggal
        ' 
        colTanggal.HeaderText = "Tanggal"
        colTanggal.MinimumWidth = 6
        colTanggal.Name = "colTanggal"
        ' 
        ' colNama
        ' 
        colNama.HeaderText = "Nama User"
        colNama.MinimumWidth = 6
        colNama.Name = "colNama"
        ' 
        ' colEmail
        ' 
        colEmail.HeaderText = "Email"
        colEmail.MinimumWidth = 6
        colEmail.Name = "colEmail"
        ' 
        ' colUcapan
        ' 
        colUcapan.HeaderText = "Anda Mengucap"
        colUcapan.MinimumWidth = 6
        colUcapan.Name = "colUcapan"
        ' 
        ' colSkor
        ' 
        colSkor.HeaderText = "Skor"
        colSkor.MinimumWidth = 6
        colSkor.Name = "colSkor"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.IndianRed
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.Black
        Button1.Location = New Point(959, 61)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(86, 31)
        Button1.TabIndex = 1
        Button1.Text = "Back"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.Controls.Add(Button1)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(DataGridView1)
        Panel1.Location = New Point(12, 4)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1066, 545)
        Panel1.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        Label1.Location = New Point(14, 17)
        Label1.Name = "Label1"
        Label1.Size = New Size(146, 41)
        Label1.TabIndex = 3
        Label1.Text = "HISTORY"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(50), CByte(40), CByte(100))
        Panel2.Controls.Add(Panel1)
        Panel2.Location = New Point(155, 113)
        Panel2.Margin = New Padding(3, 4, 3, 4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1081, 553)
        Panel2.TabIndex = 3
        ' 
        ' Form4
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(108), CByte(92), CByte(231))
        ClientSize = New Size(1345, 748)
        Controls.Add(Panel2)
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form4"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Halaman History"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents colNo As DataGridViewTextBoxColumn
    Friend WithEvents colTanggal As DataGridViewTextBoxColumn
    Friend WithEvents colNama As DataGridViewTextBoxColumn
    Friend WithEvents colEmail As DataGridViewTextBoxColumn
    Friend WithEvents colUcapan As DataGridViewTextBoxColumn
    Friend WithEvents colSkor As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
End Class
