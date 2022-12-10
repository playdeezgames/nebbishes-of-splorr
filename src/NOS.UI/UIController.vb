Public Class UIController
    Private ReadOnly _context As IUIContext
    Private ReadOnly _world As IWorld
    Private ReadOnly _states As New Dictionary(Of UIStates, IStateController)
    Private _state As UIStates
    Sub New(context As IUIContext)
        _context = context
        _world = New World
        AddHandler context.OnUpdate, AddressOf OnUpdate
        AddHandler context.OnKey, AddressOf OnKey
        _states.Add(UIStates.Title, New TitleStateController(_context, _world))
        _state = UIStates.Title
    End Sub

    Private Sub OnKey(keyName As String)
        _states(_state).HandleKey(keyName)
    End Sub

    Private Sub OnUpdate()
        _context.Fill(0, 0, _context.ViewWidth, _context.ViewHeight, Hue.Black)
        _states(_state).Update()
    End Sub
End Class
