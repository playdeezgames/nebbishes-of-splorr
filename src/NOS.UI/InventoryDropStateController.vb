Friend Class InventoryDropStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
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
        DefaultFont.WriteLine((0, 0), LineSize, "Drop Items:", Hue.White)
    End Sub
End Class
