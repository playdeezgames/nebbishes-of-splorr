Public Class BaseThingie
    Implements IBaseThingie
    Protected ReadOnly _worldData As WorldData
    Public ReadOnly Property World As IWorld Implements IBaseThingie.World
    Friend Sub New(worldData As WorldData, world As World)
        _worldData = worldData
        Me.World = world
    End Sub
End Class
