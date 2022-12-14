Friend Class SaveStateController
    Inherits BaseStateController
    Private _menu As Menu

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(_context, DefaultFontName, (0, 6), LineSize, (Hue.White, Hue.Black), 5, "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5")
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.GameMenu)
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EnterKeyName, SpaceKeyName
                _world.Save(_menu.CurrentItem + 1)
                SetState(UIStates.GameMenu)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        DefaultFont.Write((0, 0), "Save To...", Hue.Blue)
        _menu.Draw()
    End Sub
End Class
