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

    Protected MustOverride Sub HandleKey(keyName As String)

    Public Sub Update(ticks As Long) Implements IStateController.Update
        While _context.HasKey
            HandleKey(_context.ReadKey)
        End While
        Redraw(ticks)
    End Sub
    Protected MustOverride Sub Redraw(ticks As Long)
    Protected Sub SetState(uiState As UIStates)
        _context.FlushKeys()
        RaiseEvent ChangeState(uiState)
    End Sub
End Class
