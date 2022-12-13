Friend Class HelpStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        SetState(UIStates.InPlay)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = font.WriteLine((0, 0), (160, 6), "Help:", Hue.White)
        y = font.WriteLine((0, y), (160, 6), "Arrows - Move", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), "C - Character Status", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), "F - Forage", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), "H - Help", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), "Z - Zleep", Hue.Blue)
    End Sub
End Class
