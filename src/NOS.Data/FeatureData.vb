Public Class FeatureData
    Property FeatureType As Integer
    Property ItemIds As New HashSet(Of Integer)
    Property Timers As New Dictionary(Of Integer, Integer())
End Class
