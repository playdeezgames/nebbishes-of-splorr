Friend Class ConfirmQuitStateController
    Inherits BaseStateController
    Private _menu As Menu

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(context, DefaultFontName, (0, 6), (160, 6), (Hue.White, Hue.Black), 2, "No", "Yes")
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case NorthKeyName, SouthKeyName
                _menu.NextItem()
            Case EnterKeyName, SpaceKeyName
                Select Case _menu.CurrentItem
                    Case 0
                        SetState(UIStates.MainMenu)
                    Case 1
                        _context.SignalExit()
                End Select
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.Write((0, 0), "Are you sure you want to quit?", Hue.Red)
        _menu.Draw()
    End Sub
End Class
