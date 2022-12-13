Friend Class Location
    Implements ILocation
    Private _worldData As WorldData
    Public ReadOnly Property Id As Integer Implements ILocation.Id

    Public ReadOnly Property Name As String Implements ILocation.Name
        Get
            Return _worldData.Locations(Id).Name
        End Get
    End Property

    Public ReadOnly Property Routes As IEnumerable(Of IRoute) Implements ILocation.Routes
        Get
            Return _worldData.Locations(Id).Routes.Select(Function(x) New Route(_worldData, Id, CType(x.Key, Directions)))
        End Get
    End Property

    Public ReadOnly Property Route(direction As Directions) As IRoute Implements ILocation.Route
        Get
            If Not HasRoute(direction) Then
                Return Nothing
            End If
            Return New Route(_worldData, Id, direction)
        End Get
    End Property

    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Shared Function Create(worldData As WorldData, name As String, locationType As LocationTypes) As Location
        Dim id = If(worldData.Locations.Any, worldData.Locations.Keys.Max + 1, 0)
        worldData.Locations.Add(id, New LocationData With
                                {
                                    .Name = name,
                                    .LocationType = locationType
                                })
        Return New Location(worldData, id)
    End Function

    Public Function HasRoute(direction As Directions) As Boolean Implements ILocation.HasRoute
        Return _worldData.Locations(Id).Routes.ContainsKey(direction)
    End Function
End Class
