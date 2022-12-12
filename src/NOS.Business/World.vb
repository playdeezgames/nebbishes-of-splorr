Public Class World
    Implements IWorld
    Private _worldData As WorldData
    Private _random As New Random

    Public ReadOnly Property IsInPlay As Boolean Implements IWorld.IsInPlay
        Get
            Return _worldData IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property PlayerCharacter As ICharacter Implements IWorld.PlayerCharacter
        Get
            Return New Character(_worldData, _worldData.PlayerCharacterId.Value)
        End Get
    End Property

    Public ReadOnly Property Locations As IEnumerable(Of ILocation) Implements IWorld.Locations
        Get
            Dim result As New List(Of ILocation)
            For index = 0 To _worldData.Locations.Count - 1
                result.Add(New Location(_worldData, index))
            Next
            Return result
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
        CreateOverworld()
        CreatePlayerCharacter()
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim candidates = Locations.ToList()
        Dim character = CreateCharacter("you", candidates(_random.Next(candidates.Count)))
        character.SetAsPlayerCharacter()
    End Sub

    Private Const WorldColumns = 32
    Private Const WorldRows = 32
    Private Sub CreateOverworld()
        Dim overworld As New Dictionary(Of (Integer, Integer), ILocation)
        For column = 0 To WorldColumns - 1
            For row = 0 To WorldRows - 1
                overworld.Add((column, row), CreateLocation($"({column}, {row})"))
            Next
        Next
        For column = 0 To WorldColumns - 1
            For row = 0 To WorldRows - 1
                Dim start = overworld((column, row))
                If row > 0 Then
                    CreateRoute(start, Directions.North, overworld((column, row - 1)))
                End If
                If column < WorldColumns - 1 Then
                    CreateRoute(start, Directions.East, overworld((column + 1, row)))
                End If
                If row < WorldRows - 1 Then
                    CreateRoute(start, Directions.South, overworld((column, row + 1)))
                End If
                If column > 0 Then
                    CreateRoute(start, Directions.West, overworld((column - 1, row)))
                End If
            Next
        Next
    End Sub

    Private Function CreateRoute(start As ILocation, direction As Directions, finish As ILocation) As IRoute
        Return Route.Create(_worldData, start, direction, finish)
    End Function

    Private Function CreateLocation(name As String) As ILocation
        Return Location.Create(_worldData, name)
    End Function
    Private Function CreateCharacter(name As String, location As ILocation) As ICharacter
        Return Character.Create(_worldData, name, location)
    End Function
End Class
