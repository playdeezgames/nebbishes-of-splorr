Public Class Location
    Private _worldData As WorldData
    Public ReadOnly Id As Integer
    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Shared Function Create(worldData As WorldData) As Location
        Dim id = worldData.Locations.Count
        worldData.Locations.Add(New LocationData)
        Return New Location(worldData, id)
    End Function
End Class
