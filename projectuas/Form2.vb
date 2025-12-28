Imports System.Windows.Forms
Imports System.Speech.Recognition ' Wajib untuk Pengenalan Suara Lokal
Imports System.Drawing

Public Class Form2

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
    Private _namaPengguna As String
    Private _userId As Integer          ' Diterima dari Form1
    Private _teksDiucapkan As String = ""
    Private recognizer As SpeechRecognitionEngine
    Private currentQuestion As QuestionData ' Menggunakan Structure dari DatabaseModule

    ' NEW: Audio Recording
    Private audioRecorder As AudioRecorder
    Private recordedWavPath As String = ""

    ' UI Component Names: Label3=Hi User, Label2=Teks Target, Button1=Tap to Speak, Button2=Next

    ' =======================================================
    ' 1. CONSTRUCTOR
    ' =======================================================
    Public Sub New(ByVal nama As String, ByVal userId As Integer)
        InitializeComponent()
        _namaPengguna = nama
        _userId = userId
    End Sub

    ' =======================================================
    ' 2. FORM LOAD (Mengambil Pertanyaan Baru dan Inisialisasi)
    ' =======================================================
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' --- AMBIL PERTANYAAN DARI DATABASE ---
        currentQuestion = DatabaseModule.GetRandomQuestion()

        If currentQuestion.ID <= 0 OrElse String.IsNullOrEmpty(currentQuestion.Text) Then
            MessageBox.Show("Gagal mengambil pertanyaan dari database. Pastikan tabel 'questions' sudah terisi!", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' Tetapkan nilai default aman jika DB gagal atau kosong
            currentQuestion.Text = "Cup"
            currentQuestion.ID = 1
            currentQuestion.Level = "Default"
            Button1.Enabled = False
        End If

        ' Set Judul "Hi, User" (Label3)
        UpdateStatus("Loading...")
        ' Set Teks Target (Label2) dari Database
        Label2.Text = currentQuestion.Text.ToUpper()

        Button2.Enabled = False

        InitializeSpeechRecognizer()
    End Sub

    ' =======================================================
    ' 3. INISIALISASI SYSTEM.SPEECH
    ' =======================================================
    Private Sub InitializeSpeechRecognizer()
        ' Pastikan recognizer lama dibersihkan jika form di-load ulang
        If recognizer IsNot Nothing Then recognizer.Dispose()

        Try
            recognizer = New SpeechRecognitionEngine()

            ' Menggunakan Teks Target yang berasal dari DB
            Dim grammarBuilder As New GrammarBuilder(currentQuestion.Text)
            Dim specificGrammar As New Grammar(grammarBuilder)

            recognizer.LoadGrammar(specificGrammar)

            recognizer.SetInputToDefaultAudioDevice()

            ' Memasangkan Event Handlers
            AddHandler recognizer.SpeechRecognized, AddressOf Recognizer_SpeechRecognized
            AddHandler recognizer.RecognizeCompleted, AddressOf Recognizer_RecognizeCompleted

            UpdateStatus("Recognizer siap. Tekan 'Tap to Speak'.")
            Label3.ForeColor = Color.Navy

        Catch ex As Exception
            MessageBox.Show($"Gagal inisialisasi Speech Recognizer: {ex.Message}", "Speech Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Button1.Enabled = False
            UpdateStatus("ERROR: Pengenalan suara tidak tersedia.")
            Label3.ForeColor = Color.Red
        End Try
    End Sub

    ' =======================================================
    ' 4. EVENT CLICK: TOMBOL MULAI/STOP (Button1 - Start/Stop)
    ' =======================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Jika sedang merekam/mendengarkan, maka ini bertindak sebagai tombol STOP
        If audioRecorder IsNot Nothing AndAlso audioRecorder.IsRecording Then
            UpdateStatus("Stopping... Please wait.")
            Label3.ForeColor = Color.Brown

            ' NEW: Stop audio recording immediately
            Try
                If audioRecorder IsNot Nothing AndAlso audioRecorder.IsRecording Then
                    recordedWavPath = audioRecorder.StopRecording()
                    Debug.WriteLine($"Audio saved: {recordedWavPath}")
                End If
            Catch recEx As Exception
                Debug.WriteLine($"Error stopping recording: {recEx.Message}")
            End Try

            ' Hentikan pengenalan suara (akan memicu RecognizeCompleted)
            If recognizer IsNot Nothing Then
                recognizer.RecognizeAsyncCancel()
            End If

            ' NEW: Enable Next button immediately and reset button appearance
            Button1.Text = "TAP TO SPEAK"
            Button1.BackColor = SystemColors.Control
            Button1.ForeColor = SystemColors.ControlText
            Button1.Enabled = True
            Button2.Enabled = True

            ' Set default text if nothing recognized yet
            If String.IsNullOrEmpty(_teksDiucapkan) Then
                _teksDiucapkan = "(Manual Stop)"
            End If

            UpdateStatus("Recording stopped. Click NEXT to proceed to scoring.")
            Label3.ForeColor = Color.Blue

            Exit Sub
        End If

        If recognizer Is Nothing Then
            InitializeSpeechRecognizer()
            If recognizer Is Nothing Then Exit Sub
        End If

        Try
            ' Reset status
            _teksDiucapkan = ""
            recordedWavPath = ""

            ' NEW: Start audio recording
            Try
                audioRecorder = New AudioRecorder()
                audioRecorder.StartRecording()
            Catch recEx As Exception
                MessageBox.Show($"Warning: Audio recording failed: {recEx.Message}", "Recording Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            Button1.Text = "STOP" ' Ubah tombol jadi STOP
            Button1.BackColor = Color.Red
            Button1.ForeColor = Color.White

            Button2.Enabled = False
            UpdateStatus("Recording and listening... Speak now!")
            Label3.ForeColor = Color.DarkOrange

            recognizer.RecognizeAsync(RecognizeMode.Single)

        Catch ex As Exception
            MessageBox.Show($"Error saat memulai mendengarkan: {ex.Message}", "Recognition Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Button1.Enabled = True
            UpdateStatus("Kesalahan saat mencoba mendengarkan.")
            Label3.ForeColor = Color.Red
        End Try
    End Sub

    ' =======================================================
    ' 5. EVENT HANDLERS SYSTEM.SPEECH
    ' =======================================================

    ' Event saat suara dikenali
    Private Sub Recognizer_SpeechRecognized(sender As Object, e As SpeechRecognizedEventArgs)
        _teksDiucapkan = e.Result.Text

        If e.Result.Confidence < 0.7 Then
            UpdateStatus($"Recognized (Low Confidence: {e.Result.Confidence:P}): '{_teksDiucapkan}'")
            Label3.ForeColor = Color.DarkRed
        Else
            UpdateStatus($"Recognized (High Confidence: {e.Result.Confidence:P}): '{_teksDiucapkan}'")
            Label3.ForeColor = Color.Green
        End If
    End Sub

    ' Event saat proses pengenalan selesai
    Private Sub Recognizer_RecognizeCompleted(sender As Object, e As RecognizeCompletedEventArgs)
        ' NEW: Stop audio recording
        Try
            If audioRecorder IsNot Nothing AndAlso audioRecorder.IsRecording Then
                recordedWavPath = audioRecorder.StopRecording()
                Debug.WriteLine($"Audio saved: {recordedWavPath}")
            End If
        Catch recEx As Exception
            Debug.WriteLine($"Error stopping recording: {recEx.Message}")
        End Try

        ' Kembalikan tombol ke kondisi awal
        Button1.Text = "TAP TO SPEAK"
        Button1.BackColor = SystemColors.Control
        Button1.ForeColor = SystemColors.ControlText
        Button1.Enabled = True

        Button2.Enabled = True

        If e.Error IsNot Nothing Then
            _teksDiucapkan = $"[ERROR SISTEM: {e.Error.Message}]"
            UpdateStatus($"ERROR SISTEM: {e.Error.Message}")
            Label3.ForeColor = Color.Red
        ElseIf e.Cancelled Then
            ' JIKA DIHENTIKAN MANUAL
            If String.IsNullOrEmpty(_teksDiucapkan) Then
                _teksDiucapkan = "(Manual Stop)"
            End If
            UpdateStatus("Pengenalan dihentikan manual. Klik NEXT untuk cek skor suara.")
            Label3.ForeColor = Color.Blue
        ElseIf String.IsNullOrEmpty(_teksDiucapkan) OrElse e.Result Is Nothing Then
            _teksDiucapkan = "(No Speech Detected)"
            UpdateStatus("Suara tidak terdeteksi. Klik NEXT untuk tetap mencoba DTW.")
            Label3.ForeColor = Color.Gray
        End If

        If Not _teksDiucapkan.StartsWith("[") AndAlso Not _teksDiucapkan.StartsWith("(") Then
            UpdateStatus($"Selesai. Teks Anda: '{_teksDiucapkan}'. Siap ke scoring.")
            Label3.ForeColor = Color.Blue
        End If

    End Sub

    ' =======================================================
    ' 6. EVENT CLICK: TOMBOL NEXT/SCORING (Button2)
    ' =======================================================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Izinkan lanjut jika minimal sudah ada file audio
        If String.IsNullOrEmpty(recordedWavPath) OrElse Not System.IO.File.Exists(recordedWavPath) Then
            MessageBox.Show("Harap rekam suara terlebih dahulu.", "Warning")
            Exit Sub
        End If

        ' Hentikan semua operasi pengenalan sebelum navigasi
        If recognizer IsNot Nothing Then
            recognizer.RecognizeAsyncCancel()
        End If

        Me.Hide()
        ' NEW: Pass WAV path to Form3
        Dim frmScoring As New Form3(_namaPengguna, currentQuestion.Text, _teksDiucapkan, _userId, currentQuestion.ID, recordedWavPath)
        frmScoring.Show()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    ' Button History Click
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Dim frmHistory As New Form4(_userId)
        frmHistory.ShowDialog()
    End Sub

    ' CATATAN: HAPUS Protected Overrides Sub Dispose(disposing As Boolean) dari sini 
    ' karena kemungkinan sudah dideklarasikan di Form2.Designer.vb.

    Private Sub UpdateStatus(ByVal message As String)
        Dim level As String = If(currentQuestion.Level, "?")
        Label3.Text = $"Hi, {_namaPengguna} ({level}) | {message}"
    End Sub

End Class