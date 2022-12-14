Friend Class InventoryStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = font.WriteLine((0, 0), (160, 6), "Inventory:", Hue.White)
        Dim itemGroups = _world.PlayerCharacter.Items.GroupBy(Function(x) x.ItemType)
        If Not itemGroups.Any Then
            font.WriteLine((0, y), (160, 6), "(nothing)", Hue.Red)
            Return
        End If
        For Each itemGroup In itemGroups
            y = font.WriteLine((0, y), (160, 6), $"{itemGroup.Key.Name}(x{itemGroup.Count})", Hue.Blue)
        Next
    End Sub
End Class
