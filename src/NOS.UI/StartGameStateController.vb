Friend Class StartGameStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        SetState(UIStates.MainMenu)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.WriteString(0, 0, "TODO: put prolog here", Hue.White)
        font.WriteString(0, 84, "Press any key.", Hue.White)
    End Sub

    Public Overrides Sub Restart()
        _world.Start()
        MyBase.Restart()
    End Sub
End Class
