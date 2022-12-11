Friend Class MainMenuStateController
    Inherits BaseStateController
    Private _menu As Menu

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(context, DefaultFontName, (0, 6), (160, 6), (Hue.White, Hue.Black), "New Game", "Load Game", "Options", "Quit")
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case "Up"
                _menu.PreviousItem()
            Case "Down"
                _menu.NextItem()
            Case "Enter", "Space"
                Select Case _menu.CurrentItem
                    Case 2
                        SetState(UIStates.Options)
                    Case 3
                        If _world.IsInPlay Then
                        Else
                            SetState(UIStates.ConfirmQuit)
                        End If
                End Select
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.WriteString(0, 0, "Main Menu:", Hue.Blue)
        _menu.Draw()
    End Sub
End Class
