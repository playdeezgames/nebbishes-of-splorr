Friend Interface IStateController
    Sub Restart()
    Sub Update()
    Event ChangeState(uiState As UIStates)
End Interface
