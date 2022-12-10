Friend Class MainMenuStateController
    Inherits BaseStateController
    Private _item As Integer

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case "Up"
                _item = (_item + 2) Mod 3
            Case "Down"
                _item = (_item + 1) Mod 3
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.WriteString(0, 0, "Main Menu:", Hue.Blue)
        _context.Fill(0, _item * 6 + 6, 160, 6, Hue.White)
        font.WriteString(0, 6, If(_world.IsInPlay, "Continue", "New Game"), If(_item = 0, Hue.Black, Hue.White))
        font.WriteString(0, 12, If(_world.IsInPlay, "Save Game", "Load Game"), If(_item = 1, Hue.Black, Hue.White))
        font.WriteString(0, 18, If(_world.IsInPlay, "Abandon Game", "Quit"), If(_item = 2, Hue.Black, Hue.White))
    End Sub
End Class
