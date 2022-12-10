Public Class World
    Implements IWorld

    Public ReadOnly Property IsInPlay As Boolean Implements IWorld.IsInPlay
        Get
            Return False
        End Get
    End Property
End Class
