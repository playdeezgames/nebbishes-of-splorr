Friend Interface IStateController
    Sub Restart()
    Sub Update(ticks As Long)
    Event ChangeState(uiState As UIStates)
End Interface
