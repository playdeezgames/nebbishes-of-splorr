Public Class UIController
    Private ReadOnly _context As IUIContext
    Sub New(context As IUIContext)
        _context = context
        AddHandler context.OnUpdate, AddressOf OnUpdate
        AddHandler context.OnKey, AddressOf OnKey
    End Sub

    Private Sub OnKey(keyName As String)
        Select Case keyName
            Case "Escape"
                _context.SignalExit()
        End Select
    End Sub

    Private Sub OnUpdate()
        _context.Fill(0, 0, _context.ViewWidth, _context.ViewHeight, Hue.Black)
        _context.SetPixel(0, 0, Hue.Blue)
    End Sub
End Class
