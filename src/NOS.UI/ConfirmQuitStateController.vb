Friend Class ConfirmQuitStateController
    Inherits BaseStateController
    Dim _item As Integer

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case "Up", "Down"
                _item = 1 - _item
            Case "Enter", "Space"
                Select Case _item
                    Case 0
                        SetState(UIStates.MainMenu)
                    Case 1
                        _context.SignalExit()
                End Select
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.WriteString(0, 0, "Are you sure you want to quit?", Hue.Red)
        _context.Fill(0, _item * 6 + 6, 160, 6, Hue.White)
        font.WriteString(0, 6, "No", If(_item = 0, Hue.Black, Hue.White))
        font.WriteString(0, 12, "Yes", If(_item = 1, Hue.Black, Hue.White))
    End Sub
End Class
