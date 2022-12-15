Friend Class OptionsStateController
    Inherits BaseStateController
    Private ReadOnly _menu As Menu

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(context, DefaultFontName, (0, 6), (160, 6), (Hue.White, Hue.Black), 6, " 640 x  360", "1280 x  720", "1920 x 1080", "2560 x 1440", "3200 x 1800", "3840 x 1960")
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EscapeKeyName
                If _world.IsInPlay Then
                    SetState(UIStates.GameMenu)
                Else
                    SetState(UIStates.MainMenu)
                End If
            Case EnterKeyName, SpaceKeyName
                _context.SignalUIScale(_menu.CurrentItem * 4 + 4)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.Write((0, 0), "Screen Size:", Hue.Blue)
        _menu.Draw()
    End Sub
End Class
