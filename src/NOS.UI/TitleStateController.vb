Friend Class TitleStateController
    Implements IStateController
    Private _context As IUIContext
    Private _world As IWorld
    Public Sub New(context As IUIContext, world As IWorld)
        _context = context
        _world = world
    End Sub

    Public Sub Update() Implements IStateController.Update
        _context.GetFont(DefaultFontName).WriteString(0, 0, " !""#$%&'()*+,-./", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 6, "0123456789:;<=>?", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 12, "@ABCDEFGHIJKLMNO", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 18, "PQRSTUVWXYZ[\]^_", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 24, "`abcdefghijklmno", Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 30, "pqrstuvwxyz{|}~" + ChrW(127), Hue.White)
        _context.GetFont(DefaultFontName).WriteString(0, 42, "Nebbishes of SPLORR!!", Hue.Blue)
    End Sub

    Public Sub HandleKey(keyName As String) Implements IStateController.HandleKey
    End Sub
End Class
