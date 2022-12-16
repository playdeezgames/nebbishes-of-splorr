Friend Class Route
    Implements IRoute
    Private ReadOnly _worldData As WorldData
    Private ReadOnly _world As World

    Public Sub New(worldData As WorldData, world As World, locationId As Integer, direction As Directions)
        _worldData = worldData
        _world = world
        Me.LocationId = locationId
        Me.Direction = direction
    End Sub

    Public ReadOnly Property LocationId As Integer Implements IRoute.LocationId
    Public ReadOnly Property Direction As Directions Implements IRoute.Direction

    Public ReadOnly Property FromLocation As ILocation Implements IRoute.FromLocation
        Get
            Return New Location(_worldData, _world, LocationId)
        End Get
    End Property

    Public ReadOnly Property ToLocation As ILocation Implements IRoute.ToLocation
        Get
            Return New Location(_worldData, _world, _worldData.Locations(LocationId).Routes(Direction).ToLocationId)
        End Get
    End Property

    Friend Shared Function Create(worldData As WorldData, world As World, start As ILocation, direction As Directions, finish As ILocation) As IRoute
        worldData.Locations(start.Id).Routes(direction) = New RouteData With {.ToLocationId = finish.Id}
        Return New Route(worldData, world, start.Id, direction)
    End Function
End Class
