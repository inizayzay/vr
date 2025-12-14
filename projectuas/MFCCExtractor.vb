
Imports NAudio.Wave
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Math

Public Class MFCCExtractor

    ' ===================================================
    ' CONSTANTS
    ' ===================================================
    Private Const N_MFCC As Integer = 13
    Private Const SAMPLE_RATE As Integer = 22050
    Private Const FRAME_LENGTH As Integer = 2048
    Private Const HOP_LENGTH As Integer = 512
    Private Const N_MEL As Integer = 40

    ' ===================================================
    ' EXTRACT MFCC FROM WAV FILE (Simplified)
    ' ===================================================
    Public Shared Function ExtractMFCC(wavPath As String) As Double(,)
        Try
            ' Validate file exists
            If Not File.Exists(wavPath) Then
                Throw New FileNotFoundException($"WAV file not found: {wavPath}")
            End If

            ' Load WAV file using NAudio
            Dim audioData() As Single = LoadWavFile(wavPath)

            ' Calculate MFCC (simplified version)
            ' For production, you would use a proper MFCC library
            ' This is a placeholder that returns dummy data
            ' You should replace this with actual MFCC calculation

            ' For now, return a simple feature matrix based on audio statistics
            Dim numFrames As Integer = CInt((audioData.Length - FRAME_LENGTH) / HOP_LENGTH) + 1
            Dim mfcc(N_MFCC - 1, numFrames - 1) As Double

            ' Simple feature extraction (placeholder)
            For i As Integer = 0 To numFrames - 1
                Dim frameStart As Integer = i * HOP_LENGTH
                Dim frameEnd As Integer = Math.Min(frameStart + FRAME_LENGTH, audioData.Length)

                ' Extract simple features from frame
                For j As Integer = 0 To N_MFCC - 1
                    ' Simple energy-based features (placeholder)
                    Dim energy As Double = 0.0
                    For k As Integer = frameStart To frameEnd - 1
                        energy += audioData(k) * audioData(k)
                    Next
                    mfcc(j, i) = Math.Log(energy + 1.0) * (j + 1) / N_MFCC
                Next
            Next

            Return mfcc

        Catch ex As Exception
            Throw New Exception($"Error extracting MFCC: {ex.Message}", ex)
        End Try
    End Function

    ' ===================================================
    ' LOAD WAV FILE USING NAUDIO
    ' ===================================================
    Private Shared Function LoadWavFile(wavPath As String) As Single()
        Using reader As New AudioFileReader(wavPath)
            ' Read all samples
            Dim sampleCount As Integer = CInt(reader.Length / 4) ' 4 bytes per float
            Dim samples(sampleCount - 1) As Single
            reader.Read(samples, 0, sampleCount)

            ' Convert to mono if stereo
            If reader.WaveFormat.Channels > 1 Then
                samples = ConvertToMono(samples, reader.WaveFormat.Channels)
            End If

            ' Resample if needed (simplified - just decimate)
            If reader.WaveFormat.SampleRate <> SAMPLE_RATE Then
                samples = SimpleResample(samples, reader.WaveFormat.SampleRate, SAMPLE_RATE)
            End If

            Return samples
        End Using
    End Function

    ' ===================================================
    ' CONVERT TO MONO
    ' ===================================================
    Private Shared Function ConvertToMono(samples() As Single, channels As Integer) As Single()
        Dim monoCount As Integer = samples.Length \ channels
        Dim mono(monoCount - 1) As Single

        For i As Integer = 0 To monoCount - 1
            Dim sum As Single = 0
            For c As Integer = 0 To channels - 1
                sum += samples(i * channels + c)
            Next
            mono(i) = sum / channels
        Next

        Return mono
    End Function

    ' ===================================================
    ' SIMPLE RESAMPLE (Decimation)
    ' ===================================================
    Private Shared Function SimpleResample(samples() As Single, fromRate As Integer, toRate As Integer) As Single()
        If fromRate = toRate Then Return samples

        Dim ratio As Double = CDbl(fromRate) / toRate
        Dim newLength As Integer = CInt(samples.Length / ratio)
        Dim resampled(newLength - 1) As Single

        For i As Integer = 0 To newLength - 1
            Dim srcIndex As Integer = CInt(i * ratio)
            If srcIndex < samples.Length Then
                resampled(i) = samples(srcIndex)
            End If
        Next

        Return resampled
    End Function

    ' ===================================================
    ' LOAD REFERENCE MFCC FROM JSON
    ' ===================================================
    Public Shared Function LoadReferenceFromJson(jsonPath As String) As Double(,)
        Try
            ' Validate file exists
            If Not File.Exists(jsonPath) Then
                Throw New FileNotFoundException($"Reference JSON not found: {jsonPath}")
            End If

            ' Read JSON
            Dim jsonText As String = File.ReadAllText(jsonPath)
            Dim jsonObj As JObject = JObject.Parse(jsonText)

            ' Extract MFCC array
            Dim mfccArray As JArray = CType(jsonObj("mfcc"), JArray)

            ' Get dimensions
            Dim nCoeffs As Integer = mfccArray.Count
            Dim timeSteps As Integer = CType(mfccArray(0), JArray).Count

            ' Convert to 2D array
            Dim mfcc(nCoeffs - 1, timeSteps - 1) As Double

            For i As Integer = 0 To nCoeffs - 1
                Dim row As JArray = CType(mfccArray(i), JArray)
                For j As Integer = 0 To timeSteps - 1
                    mfcc(i, j) = CDbl(row(j))
                Next
            Next

            Return mfcc

        Catch ex As Exception
            Throw New Exception($"Error loading reference JSON: {ex.Message}", ex)
        End Try
    End Function

End Class
