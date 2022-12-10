Friend MustInherit Class BaseStateController
    Implements IStateController
    Protected _context As IUIContext
    Protected _world As IWorld
    Public Sub New(context As IUIContext, world As IWorld)
        _context = context
        _world = world
    End Sub

    Public Event ChangeState(uiState As UIStates) Implements IStateController.ChangeState

    Public Overridable Sub Restart() Implements IStateController.Restart

    End Sub

    Public MustOverride Sub Update() Implements IStateController.Update
End Class
