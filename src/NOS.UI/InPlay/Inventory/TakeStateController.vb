Friend Class TakeStateController
    Inherits BaseItemTransferStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world, UIStates.InPlay, UIStates.InPlay, Hue.Red)
    End Sub

    Protected Overrides ReadOnly Property Header As String
        Get
            Return $"Take Items(x{TransferTotal}):"
        End Get
    End Property

    Protected Overrides ReadOnly Property ItemSource As IEnumerable(Of IItem)
        Get
            Return _world.PlayerCharacter.Location.Items
        End Get
    End Property

    Protected Overrides Sub DoTransfer()
        _world.PlayerCharacter.AttemptTakeItems(TransferQuantities)
    End Sub

    Protected Overrides Function MenuItemText(itemName As String, availableQuantity As Integer, transferQuantity As Integer) As String
        Return $"{itemName}(leave {availableQuantity}, take {transferQuantity})"
    End Function
End Class
