﻿Friend Class DropStateHandler
    Inherits BaseItemTransferStateController
    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world, UIStates.InPlay, UIStates.InPlay, Hue.Red)
    End Sub
    Protected Overrides ReadOnly Property Header As String
        Get
            Return $"Drop Items(x{TransferTotal}):"
        End Get
    End Property
    Protected Overrides ReadOnly Property ItemSource As IEnumerable(Of IItem)
        Get
            Return _world.PlayerCharacter.Items
        End Get
    End Property
    Protected Overrides Sub DoTransfer()
        _world.PlayerCharacter.AttemptDropItems(TransferQuantities)
    End Sub
    Protected Overrides Function MenuItemText(itemName As String, availableQuantity As Integer, transferQuantity As Integer) As String
        Return $"{itemName}(keep {availableQuantity}, drop {transferQuantity})"
    End Function
End Class
