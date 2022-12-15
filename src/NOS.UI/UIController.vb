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
        AddStates()
        BootstrapStates()
    End Sub

    Private Sub BootstrapStates()
        For Each state In _states
            AddHandler state.Value.ChangeState, AddressOf OnChangeState
        Next
        OnChangeState(UIStates.Title)
    End Sub

    Private Sub AddStates()
        _states.Add(UIStates.Title, New TitleStateController(_context, _world))
        _states.Add(UIStates.MainMenu, New MainMenuStateController(_context, _world))
        _states.Add(UIStates.ConfirmQuit, New ConfirmQuitStateController(_context, _world))
        _states.Add(UIStates.Options, New OptionsStateController(_context, _world))
        _states.Add(UIStates.StartGame, New StartGameStateController(_context, _world))
        _states.Add(UIStates.InPlay, New InPlayStateController(_context, _world))
        _states.Add(UIStates.Navigation, New GeneralStateController(_context, _world))
        _states.Add(UIStates.CharacterStatus, New CharacterStatusStateController(_context, _world))
        _states.Add(UIStates.GeneralHelp, New GeneralHelpStateController(_context, _world))
        _states.Add(UIStates.GameMenu, New GameMenuStateController(_context, _world))
        _states.Add(UIStates.ConfirmAbandon, New AbandonGameStateController(_context, _world))
        _states.Add(UIStates.Inventory, New InventoryStateController(_context, _world))
        _states.Add(UIStates.InventoryHelp, New InventoryHelpStateController(_context, _world))
        _states.Add(UIStates.InventoryDrop, New InventoryDropStateController(_context, _world))
        _states.Add(UIStates.Ground, New GroundStateController(_context, _world))
        _states.Add(UIStates.Take, New TakeStateController(_context, _world))
        _states.Add(UIStates.GroundHelp, New GroundHelpStateController(_context, _world))
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
