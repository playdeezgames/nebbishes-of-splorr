Imports System.ComponentModel

Friend Class GeneralStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.GameMenu)
            Case NorthKeyName
                _world.PlayerCharacter.AttemptMove(Directions.North)
                SetState(UIStates.InPlay)
            Case EastKeyName
                _world.PlayerCharacter.AttemptMove(Directions.East)
                SetState(UIStates.InPlay)
            Case SouthKeyName
                _world.PlayerCharacter.AttemptMove(Directions.South)
                SetState(UIStates.InPlay)
            Case WestKeyName
                _world.PlayerCharacter.AttemptMove(Directions.West)
                SetState(UIStates.InPlay)
            Case CharacterStatusKeyName
                SetState(UIStates.CharacterStatus)
            Case ForageKeyName
                _world.PlayerCharacter.AttemptForage()
                SetState(UIStates.InPlay)
            Case HelpKeyName
                SetState(UIStates.GeneralHelp)
            Case DropKeyName
                SetState(UIStates.InventoryDrop)
            Case ZleepKeyName
                _world.PlayerCharacter.AttemptSleep()
                SetState(UIStates.InPlay)
            Case TakeKeyName
                SetState(UIStates.Take)
            Case UpKeyName
                _world.PlayerCharacter.AttemptMove(Directions.Up)
                SetState(UIStates.InPlay)
            Case DownKeyName
                _world.PlayerCharacter.AttemptMove(Directions.Down)
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = ShowMessage(0, font)
        Dim character = _world.PlayerCharacter
        If character.IsDead Then
            y = font.WriteLine((0, y), LineSize, $"{character.Name} dead.", Hue.Red)
        Else
            Dim location = character.Location
            y = font.WriteLine((0, y), LineSize, $"Location: { location.Name}", Hue.Blue)
            Dim routes = location.Routes
            y = font.WriteLine((0, y), LineSize, $"Exits: {String.Join(", ", routes.Select(Function(x) x.Direction.Letter))}", Hue.White)
            If location.HasItems Then
                y = font.WriteLine((0, y), LineSize, $"There's stuff on the ground.", Hue.White)
            End If
        End If
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
