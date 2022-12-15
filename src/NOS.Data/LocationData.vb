Public Class LocationData
    Public Property Name As String
    Public Property Routes As New Dictionary(Of Integer, RouteData)
    Public Property LocationType As Integer
    Public Property Statistics As New Dictionary(Of Integer, Integer)
    Public Property ItemIds As New HashSet(Of Integer)
    Public Property FeatureIds As New HashSet(Of Integer)
End Class
