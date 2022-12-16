Friend Class InteractStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                _world.InteractionFeature = Nothing
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        DefaultFont.Write((0, 0), _world.InteractionFeature.Name, Hue.Blue)
    End Sub
End Class
