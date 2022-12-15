Friend Class MainMenuStateController
    Inherits BaseStateController
    Private _menu As Menu

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(context, DefaultFontName, (0, 6), (160, 6), (Hue.White, Hue.Black), 4, "New Game", "Load Game(COMING SOON!)", "Options", "Quit")
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EnterKeyName, SpaceKeyName
                Select Case _menu.CurrentItem
                    Case 0
                        SetState(UIStates.StartGame)
                    Case 1
                        'TODO: load
                    Case 2
                        SetState(UIStates.Options)
                    Case 3
                        SetState(UIStates.ConfirmQuit)
                End Select
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.Write((0, 0), "Main Menu:", Hue.Blue)
        _menu.Draw()
    End Sub
End Class
