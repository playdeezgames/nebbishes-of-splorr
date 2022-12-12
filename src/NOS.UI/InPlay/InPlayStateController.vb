Friend Class InPlayStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
    End Sub

    Public Overrides Sub Restart()
        MyBase.Restart()
        SetState(UIStates.Navigation)
    End Sub
End Class
