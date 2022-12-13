Imports System.IO

Friend Class NavigationStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.GameMenu)
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
            Case "C"
                SetState(UIStates.CharacterStatus)
            Case "H"
                SetState(UIStates.Help)
            Case "Z"
                _world.PlayerCharacter.AttemptSleep()
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = ShowMessage(0, font)
        Dim location = _world.PlayerCharacter.Location
        y = font.WriteLine((0, y), (160, 6), $"Location: { location.Name}", Hue.Blue)
        Dim routes = location.Routes
        y = font.Write((0, y), (160, 6), $"Exits: {String.Join(", ", routes.Select(Function(x) x.Direction.Letter))}", Hue.White)
    End Sub

    Private Function ShowMessage(y As Integer, font As Font) As Integer
        Dim lines = _world.PlayerCharacter.Messages
        If lines.Any Then
            For Each line In lines
                y = font.WriteLine((0, y), (160, 6), line, Hue.White)
            Next
        End If
        Return y
    End Function
End Class
