Public MustInherit Class Thingie
    Inherits BaseThingie
    Implements IThingie
    Public ReadOnly Property Id As Integer Implements IThingie.Id
    Friend Sub New(worldData As WorldData, world As IWorld, id As Integer)
        MyBase.New(worldData, world)
        Me.Id = id
    End Sub
End Class
