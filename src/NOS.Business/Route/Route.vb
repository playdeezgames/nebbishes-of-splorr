Friend Class Route
    Inherits BaseThingie
    Implements IRoute
    Public Sub New(worldData As WorldData, world As IWorld, locationId As Integer, direction As Directions)
        MyBase.New(worldData, world)
        Me.LocationId = locationId
        Me.Direction = direction
    End Sub

    Public ReadOnly Property LocationId As Integer Implements IRoute.LocationId
    Public ReadOnly Property Direction As Directions Implements IRoute.Direction

    Public ReadOnly Property FromLocation As ILocation Implements IRoute.FromLocation
        Get
            Return New Location(_worldData, World, LocationId)
        End Get
    End Property

    Public ReadOnly Property ToLocation As ILocation Implements IRoute.ToLocation
        Get
            Return New Location(_worldData, World, _worldData.Locations(LocationId).Routes(Direction).ToLocationId)
        End Get
    End Property

    Friend Shared Function Create(worldData As WorldData, world As World, start As ILocation, direction As Directions, finish As ILocation) As IRoute
        worldData.Locations(start.Id).Routes(direction) = New RouteData With {.ToLocationId = finish.Id}
        Return New Route(worldData, world, start.Id, direction)
    End Function
End Class
