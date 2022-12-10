Public Class UIController
    Private ReadOnly _context As IUIContext
    Private ReadOnly _world As IWorld
    Sub New(context As IUIContext)
        _context = context
        _world = New World
        AddHandler context.OnUpdate, AddressOf OnUpdate
        AddHandler context.OnKey, AddressOf OnKey
    End Sub

    Private Sub OnKey(keyName As String)
        Select Case keyName
            Case "Escape"
                _context.SignalExit()
            Case "D1"
                _context.SignalUIScale(4)
            Case "D2"
                _context.SignalUIScale(8)
            Case "D3"
                _context.SignalUIScale(12)
            Case "D4"
                _context.SignalUIScale(16)
        End Select
    End Sub

    Private Sub OnUpdate()
        _context.Fill(0, 0, _context.ViewWidth, _context.ViewHeight, Hue.Black)
        _context.SetPixel(0, 0, Hue.Blue)
    End Sub
End Class
