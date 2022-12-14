Friend Class TitleStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.Write((32, 30), "*************************", Hue.Red)
        font.Write((32, 36), "*                       *", Hue.Red)
        font.Write((32, 42), "*                       *", Hue.Red)
        font.Write((32, 48), "*                       *", Hue.Red)
        font.Write((32, 54), "*************************", Hue.Red)
        font.Write((40, 42), "Nebbishes of SPLORR!!", Hue.Blue)
        font.Write((16, 84), "A production of TheGrumpyGameDev", Hue.White)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        SetState(UIStates.MainMenu)
    End Sub
End Class
