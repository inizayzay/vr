
Imports System.Math

Public Class DTWComparator
    
    ' ===================================================
    ' CALCULATE DTW DISTANCE
    ' ===================================================
    Public Shared Function CalculateDTWDistance(mfcc1 As Double(,), mfcc2 As Double(,)) As Double
        Try
            ' Get dimensions
            Dim nCoeffs1 As Integer = mfcc1.GetLength(0)
            Dim timeSteps1 As Integer = mfcc1.GetLength(1)
            Dim nCoeffs2 As Integer = mfcc2.GetLength(0)
            Dim timeSteps2 As Integer = mfcc2.GetLength(1)
            
            ' Validate same number of coefficients
            If nCoeffs1 <> nCoeffs2 Then
                Throw New ArgumentException("MFCC must have same number of coefficients")
            End If
            
            ' Initialize DTW matrix
            Dim dtw(timeSteps1, timeSteps2) As Double
            
            ' Initialize first row and column with infinity
            For i As Integer = 0 To timeSteps1
                For j As Integer = 0 To timeSteps2
                    dtw(i, j) = Double.PositiveInfinity
                Next
            Next
            dtw(0, 0) = 0
            
            ' Fill DTW matrix
            For i As Integer = 1 To timeSteps1
                For j As Integer = 1 To timeSteps2
                    ' Calculate Euclidean distance between frames
                    Dim cost As Double = EuclideanDistance(mfcc1, mfcc2, i - 1, j - 1)
                    
                    ' DTW recurrence relation
                    Dim minPrev As Double = Min3(
                        dtw(i - 1, j),      ' insertion
                        dtw(i, j - 1),      ' deletion
                        dtw(i - 1, j - 1)   ' match
                    )
                    
                    dtw(i, j) = cost + minPrev
                Next
            Next
            
            ' Return final DTW distance (normalized by path length)
            Dim pathLength As Integer = timeSteps1 + timeSteps2
            Return dtw(timeSteps1, timeSteps2) / pathLength
            
        Catch ex As Exception
            Throw New Exception($"Error calculating DTW: {ex.Message}", ex)
        End Try
    End Function
    
    ' ===================================================
    ' EUCLIDEAN DISTANCE BETWEEN TWO FRAMES
    ' ===================================================
    Private Shared Function EuclideanDistance(mfcc1 As Double(,), mfcc2 As Double(,), 
                                               frame1 As Integer, frame2 As Integer) As Double
        Dim nCoeffs As Integer = mfcc1.GetLength(0)
        Dim sum As Double = 0.0
        
        For i As Integer = 0 To nCoeffs - 1
            Dim diff As Double = mfcc1(i, frame1) - mfcc2(i, frame2)
            sum += diff * diff
        Next
        
        Return Sqrt(sum)
    End Function
    
    ' ===================================================
    ' MINIMUM OF THREE VALUES
    ' ===================================================
    Private Shared Function Min3(a As Double, b As Double, c As Double) As Double
        Return Min(Min(a, b), c)
    End Function
    
    ' ===================================================
    ' CONVERT DTW DISTANCE TO SIMILARITY SCORE (0-100)
    ' ===================================================
    Public Shared Function DistanceToSimilarity(distance As Double) As Double
        ' Adjusted for simplified MFCC extraction
        ' Typical distance range: 0-500 (based on simplified features)
        
        ' Use linear normalization with adjusted max distance
        Dim maxDistance As Double = 500.0  ' Adjusted for simplified MFCC
        
        ' Linear mapping: distance 0 = 100%, distance >= maxDistance = 0%
        Dim similarity As Double = 100.0 * (1.0 - (distance / maxDistance))
        
        ' Clamp to 0-100 range
        If similarity > 100.0 Then similarity = 100.0
        If similarity < 0.0 Then similarity = 0.0
        
        Return similarity
    End Function
    
    ' ===================================================
    ' ALTERNATIVE: LINEAR NORMALIZATION
    ' ===================================================
    Public Shared Function DistanceToSimilarityLinear(distance As Double, 
                                                       Optional maxDistance As Double = 10.0) As Double
        ' Linear normalization
        ' distance = 0 → similarity = 100
        ' distance = maxDistance → similarity = 0
        
        Dim similarity As Double = 100.0 * (1.0 - (distance / maxDistance))
        
        ' Clamp to 0-100 range
        If similarity > 100.0 Then similarity = 100.0
        If similarity < 0.0 Then similarity = 0.0
        
        Return similarity
    End Function
    
End Class
