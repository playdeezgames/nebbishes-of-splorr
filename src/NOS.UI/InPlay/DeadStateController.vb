Friend Class DeadStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.GameMenu)
            Case "C"
                SetState(UIStates.CharacterStatus)
            Case "H"
                SetState(UIStates.Help)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = font.WriteLine((0, 0), (160, 6), "Yer Dead!", Hue.Red)
    End Sub
End Class
