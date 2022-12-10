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
        _context.GetFont(DefaultFontName).WriteString(0, 0, " !""#$%&'()*+,-./", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 6, "0123456789:;<=>?", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 12, "@ABCDEFGHIJKLMNO", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 18, "PQRSTUVWXYZ[\]^_", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 24, "`abcdefghijklmno", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 30, "pqrstuvwxyz{|}~" + ChrW(127), Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 42, "HELLO, WORLD!", Hue.Blue)
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
