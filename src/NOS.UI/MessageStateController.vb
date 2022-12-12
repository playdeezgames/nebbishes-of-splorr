Friend Class MessageStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case SpaceKeyName, EnterKeyName, EscapeKeyName
                _world.PlayerCharacter.DismissMessage()
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim lines = _world.PlayerCharacter.CurrentMessage
        Dim y = 0
        For Each line In lines
            y = font.Write((0, y), (160, 6), line, Hue.White) + 6
        Next
    End Sub
End Class
