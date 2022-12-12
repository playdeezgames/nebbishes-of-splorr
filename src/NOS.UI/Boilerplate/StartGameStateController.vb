Friend Class StartGameStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        SetState(UIStates.InPlay)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        font.WriteLine((0, 0), (160, 6), "You are Tagon, one of the nebbishes of  SPLORR!! What is a nebbish? It is a     person who is pitifully ineffective. Whywould you want to play a game as a      nebbish? I suppose you don't have to    _STAY_ a nebbish....", Hue.White)
        font.Write((0, 78), "Remember: H is for Help!", Hue.Red)
        font.Write((0, 84), "Press any key.", Hue.White)
    End Sub

    Public Overrides Sub Restart()
        _world.Start()
        MyBase.Restart()
    End Sub
End Class
