Friend Class Location
    Implements ILocation
    Private _worldData As WorldData
    Public ReadOnly Property Id As Integer Implements ILocation.Id

    Public ReadOnly Property Name As String Implements ILocation.Name
        Get
            Return _worldData.Locations(Id).Name
        End Get
    End Property

    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Shared Function Create(worldData As WorldData, name As String) As Location
        Dim id = If(worldData.Locations.Any, worldData.Locations.Keys.Max + 1, 0)
        worldData.Locations.Add(id, New LocationData With {.Name = name})
        Return New Location(worldData, id)
    End Function
End Class
