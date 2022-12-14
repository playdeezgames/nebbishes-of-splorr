Friend Class InventoryDropStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Throw New NotImplementedException()
    End Sub
End Class
