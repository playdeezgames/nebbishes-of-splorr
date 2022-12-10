Public Class UIController
    Private ReadOnly _context As IUIContext
    Private ReadOnly _world As IWorld
    Private ReadOnly _states As New Dictionary(Of UIStates, IStateController)
    Private _state As UIStates
    Sub New(context As IUIContext)
        _context = context
        _world = New World
        _context.SetFont(DefaultFontName, New DefaultFont(_context))
        AddHandler context.OnUpdate, AddressOf OnUpdate
        _states.Add(UIStates.Title, New TitleStateController(_context, _world))
        _states.Add(UIStates.MainMenu, New MainMenuStateController(_context, _world))
        For Each state In _states
            AddHandler state.Value.ChangeState, AddressOf OnChangeState
        Next
        OnChangeState(UIStates.Title)
    End Sub

    Private Sub OnChangeState(uiState As UIStates)
        _state = uiState
        _states(_state).Restart()
    End Sub

    Private Sub OnUpdate(ticks As Long)
        _context.Fill(0, 0, _context.ViewWidth, _context.ViewHeight, Hue.Black)
        _states(_state).Update(ticks)
    End Sub
End Class
