Friend Class GameMenuStateController
    Inherits BaseStateController
    Private _menu As Menu

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(context, DefaultFontName, (0, 6), (160, 6), (Hue.White, Hue.Black), 4, "Continue Game", "Save Game", "Options", "Abandon Game")
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.InPlay)
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EnterKeyName, SpaceKeyName
                Select Case _menu.CurrentItem
                    Case 0
                        SetState(UIStates.InPlay)
                    Case 1
                        SetState(UIStates.Save)
                    Case 2
                        SetState(UIStates.Options)
                    Case 3
                        SetState(UIStates.ConfirmAbandon)
                End Select
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.Write((0, 0), "Game Menu:", Hue.Blue)
        _menu.Draw()
    End Sub
End Class
