
Imports NAudio.Wave
Imports System.IO

Public Class AudioRecorder
    
    ' ===================================================
    ' VARIABLES
    ' ===================================================
    Private waveIn As WaveInEvent
    Private writer As WaveFileWriter
    Private outputFilePath As String
    Private isCurrentlyRecording As Boolean = False
    
    ' ===================================================
    ' PROPERTIES
    ' ===================================================
    Public ReadOnly Property IsRecording As Boolean
        Get
            Return isCurrentlyRecording
        End Get
    End Property
    
    ' ===================================================
    ' CONSTRUCTOR
    ' ===================================================
    Public Sub New()
        ' Constructor kosong - inisialisasi dilakukan di StartRecording
    End Sub
    
    ' ===================================================
    ' START RECORDING
    ' ===================================================
    Public Sub StartRecording()
        If isCurrentlyRecording Then
            Throw New InvalidOperationException("Recording sudah berjalan!")
        End If
        
        Try
            ' Setup WaveIn (input dari microphone)
            waveIn = New WaveInEvent()
            waveIn.WaveFormat = New WaveFormat(22050, 1) ' 22050 Hz, mono
            
            ' Generate temp file path
            Dim tempDir As String = Path.Combine(Application.StartupPath, "temp")
            If Not Directory.Exists(tempDir) Then
                Directory.CreateDirectory(tempDir)
            End If
            
            outputFilePath = Path.Combine(tempDir, $"user_{DateTime.Now:yyyyMMdd_HHmmss}.wav")
            
            ' Setup WaveFileWriter
            writer = New WaveFileWriter(outputFilePath, waveIn.WaveFormat)
            
            ' Event handler untuk data available
            AddHandler waveIn.DataAvailable, AddressOf OnDataAvailable
            
            ' Mulai recording
            waveIn.StartRecording()
            isCurrentlyRecording = True
            
        Catch ex As Exception
            Throw New Exception($"Error starting recording: {ex.Message}", ex)
        End Try
    End Sub
    
    ' ===================================================
    ' DATA AVAILABLE EVENT
    ' ===================================================
    Private Sub OnDataAvailable(sender As Object, e As WaveInEventArgs)
        If writer IsNot Nothing Then
            writer.Write(e.Buffer, 0, e.BytesRecorded)
        End If
    End Sub
    
    ' ===================================================
    ' STOP RECORDING
    ' ===================================================
    Public Function StopRecording() As String
        If Not isCurrentlyRecording Then
            Throw New InvalidOperationException("Recording tidak sedang berjalan!")
        End If
        
        Try
            ' Stop recording
            waveIn.StopRecording()
            
            ' Cleanup
            If writer IsNot Nothing Then
                writer.Dispose()
                writer = Nothing
            End If
            
            If waveIn IsNot Nothing Then
                waveIn.Dispose()
                waveIn = Nothing
            End If
            
            isCurrentlyRecording = False
            
            ' Return path ke file WAV
            Return outputFilePath
            
        Catch ex As Exception
            Throw New Exception($"Error stopping recording: {ex.Message}", ex)
        End Try
    End Function
    
    ' ===================================================
    ' DISPOSE
    ' ===================================================
    Public Sub Dispose()
        If isCurrentlyRecording Then
            StopRecording()
        End If
        
        If writer IsNot Nothing Then
            writer.Dispose()
            writer = Nothing
        End If
        
        If waveIn IsNot Nothing Then
            waveIn.Dispose()
            waveIn = Nothing
        End If
    End Sub
    
End Class
