Friend Class NavigationStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim location = _world.PlayerCharacter.Location
        font.WriteString(0, 0, $"Location: { location.Name}", Hue.Blue)
        Dim routes = location.Routes
        font.WriteString(0, 6, $"Exits: {String.Join(", ", routes.Select(Function(x) x.Direction.Letter))}", Hue.White)
    End Sub
End Class
