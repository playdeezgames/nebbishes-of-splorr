Friend Class NavigationStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case UpKeyName
                _world.PlayerCharacter.AttemptMove(Directions.North)
                SetState(UIStates.InPlay)
            Case RightKeyName
                _world.PlayerCharacter.AttemptMove(Directions.East)
                SetState(UIStates.InPlay)
            Case DownKeyName
                _world.PlayerCharacter.AttemptMove(Directions.South)
                SetState(UIStates.InPlay)
            Case LeftKeyName
                _world.PlayerCharacter.AttemptMove(Directions.West)
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim location = _world.PlayerCharacter.Location
        font.Write((0, 0), $"Location: { location.Name}", Hue.Blue)
        Dim routes = location.Routes
        font.Write((0, 6), (160, 6), $"Exits: {String.Join(", ", routes.Select(Function(x) x.Direction.Letter))}", Hue.White)
    End Sub
End Class
