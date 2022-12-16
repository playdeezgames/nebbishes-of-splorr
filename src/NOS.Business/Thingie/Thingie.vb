Public MustInherit Class Thingie
    Implements IThingie
    Protected ReadOnly _worldData As WorldData
    Protected ReadOnly _world As World
    Public ReadOnly Property Id As Integer Implements IThingie.Id
    Friend Sub New(worldData As WorldData, world As World, id As Integer)
        _worldData = worldData
        _world = world
        Me.Id = id
    End Sub
End Class
