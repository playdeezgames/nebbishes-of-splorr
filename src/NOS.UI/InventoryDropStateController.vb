Friend Class InventoryDropStateController
    Inherits BaseStateController
    Private ReadOnly _menu As Menu
    'Private _menuTable As New Dictionary(Of Integer, ItemTypes)

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(_context, DefaultFontName, (0, 6), LineSize, (Hue.White, Hue.Black), 14, Array.Empty(Of String))
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.Inventory)
            Case HelpKeyName
                SetState(UIStates.InventoryHelp)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        DefaultFont.WriteLine((0, 0), LineSize, "Drop Items:", Hue.Red)
        _menu.Draw()
    End Sub
    Public Overrides Sub Restart()
        MyBase.Restart()
        _menu.Items = _world.PlayerCharacter.Items.GroupBy(Function(x) x.ItemType).Select(Function(x) $"{x.Key.Name}(x{x.Count})")
    End Sub
End Class
