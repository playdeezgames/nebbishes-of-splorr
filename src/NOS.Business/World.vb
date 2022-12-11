Public Class World
    Implements IWorld
    Private _worldData As WorldData

    Public ReadOnly Property IsInPlay As Boolean Implements IWorld.IsInPlay
        Get
            Return _worldData IsNot Nothing
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
    End Sub
End Class
