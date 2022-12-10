Friend Class TitleStateController
    Implements IStateController
    Private _context As IUIContext
    Private _world As IWorld
    Private _x As Integer
    Private _y As Integer

    Public Sub New(context As IUIContext, world As IWorld)
        _context = context
        _world = world
        _x = 0
        _y = 0
    End Sub

    Public Sub Update() Implements IStateController.Update
        _context.DrawGlyph(0, 0, Hue.White, New List(Of (Integer, Integer)) From
                           {
                            (0, 0), (2, 0),
                            (0, 1), (2, 1),
                            (0, 2), (1, 2), (2, 2),
                            (0, 3), (2, 3),
                            (0, 4), (2, 4)
                            })
        _context.SetPixel(_x, _y, Hue.Blue)
    End Sub

    Public Sub HandleKey(keyName As String) Implements IStateController.HandleKey
        Select Case keyName
            Case "Up"
                _y -= 1
            Case "Down"
                _y += 1
            Case "Right"
                _x += 1
            Case "Left"
                _x -= 1
        End Select
    End Sub
End Class
