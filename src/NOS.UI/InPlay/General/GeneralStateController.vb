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
            Case InteractKeyName
                Interact()
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

    Private Sub Interact()
        Dim character = _world.PlayerCharacter
        If Not character.Location.Features.Any Then
            character.DismissMessages()
            character.AddMessage($"{character.Name} sees nothing to interact with.")
            Return
        End If
        _world.InteractionFeature = character.Location.Features.First
        SetState(UIStates.InPlay)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim y = ShowMessage(0, font)
        Dim character = _world.PlayerCharacter
        If character.IsDead Then
            y = font.WriteLine((0, y), LineSize, $"{character.Name} dead.", Hue.Red)
        Else
            Dim location As ILocation = character.Location
            y = ShowLocation(font, y, location)
            y = ShowFeatures(font, y, location)
            y = ShowRoutes(font, y, location)
            y = ShowGround(font, y, location)
        End If
    End Sub

    Private Function ShowFeatures(font As Font, y As Integer, location As ILocation) As Integer
        Dim features = location.Features
        If features.Any Then
            y = font.WriteLine((0, y), LineSize, $"Features: {String.Join(", ", features.Select(Function(x) x.Name))}", Hue.Blue)
        End If
        Return y
    End Function

    Private Shared Function ShowGround(font As Font, y As Integer, location As ILocation) As Integer
        If location.HasItems Then
            y = font.WriteLine((0, y), LineSize, $"There's stuff on the ground.", Hue.White)
        End If
        Return y
    End Function

    Private Shared Function ShowLocation(font As Font, ByRef y As Integer, ByRef location As ILocation) As Integer
        Return font.WriteLine((0, y), LineSize, $"Location: { location.Name}", Hue.Blue)
    End Function

    Private Shared Function ShowRoutes(font As Font, y As Integer, location As ILocation) As Integer
        Dim routes = location.Routes
        Return font.WriteLine((0, y), LineSize, $"Exits: {String.Join(", ", routes.Select(Function(x) x.Direction.Letter))}", Hue.White)
    End Function

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
