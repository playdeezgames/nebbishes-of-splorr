Friend Class GeneralHelpStateController
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
        y = font.WriteLine((0, y), (160, 6), "↑↓←→ - Move", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{CharacterStatusKeyName} - Character Status", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{DropKeyName} - Drop", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{EatKeyName} - Eat", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{ForageKeyName} - Forage", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{HelpKeyName} - Help", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{TakeKeyName} - Take", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{UpKeyName} - Up", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{DownKeyName} - doWn", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"{ZleepKeyName} - Zleep", Hue.Blue)
    End Sub
End Class
