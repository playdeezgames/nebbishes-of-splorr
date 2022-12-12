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
        If _world.PlayerCharacter.HasMessages Then
            SetState(UIStates.Message)
        Else
            SetState(UIStates.Navigation)
        End If
    End Sub
End Class
