Friend Class EatStateController
    Inherits BaseItemTransferStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world, UIStates.InPlay, UIStates.InPlay, Hue.Blue)
    End Sub

    Protected Overrides ReadOnly Property Header As String
        Get
            Return $"Eat(x{TransferTotal})"
        End Get
    End Property

    Protected Overrides ReadOnly Property ItemSource As IEnumerable(Of IItem)
        Get
            Return _world.PlayerCharacter.Items.Where(Function(x) x.CanEat)
        End Get
    End Property

    Protected Overrides Sub DoTransfer()
        _world.PlayerCharacter.AttemptEat(TransferQuantities)
    End Sub

    Protected Overrides Function MenuItemText(itemName As String, availableQuantity As Integer, transferQuantity As Integer) As String
        Return $"{itemName}(keep {availableQuantity}, eat {transferQuantity})"
    End Function
End Class
