Friend Class GroundHelpStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.Ground)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = font.WriteLine((0, 0), (160, 6), "Ground Help:", Hue.White)
        y = font.WriteLine((0, y), (160, 9), $"{HelpKeyName} - Help", Hue.Blue)
        y = font.WriteLine((0, y), (160, 9), $"{GroundTakeKeyName} - Take Items", Hue.Blue)
    End Sub
End Class
