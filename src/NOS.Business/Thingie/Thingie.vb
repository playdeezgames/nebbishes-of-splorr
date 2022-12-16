Public MustInherit Class Thingie
    Implements IThingie
    Protected ReadOnly _worldData As WorldData
    Public ReadOnly Property Id As Integer Implements IThingie.Id
    Public ReadOnly Property World As IWorld Implements IThingie.World
    Friend Sub New(worldData As WorldData, world As World, id As Integer)
        _worldData = worldData
        Me.World = world
        Me.Id = id
    End Sub
End Class
