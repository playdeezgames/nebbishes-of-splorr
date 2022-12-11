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
        Dim character = CreateCharacter(candidates(_random.Next(candidates.Count)))
        character.SetAsPlayerCharacter()
    End Sub

    Private Const WorldColumns = 16
    Private Const WorldRows = 16
    Private Sub CreateOverworld()
        For column = 0 To WorldColumns - 1
            For row = 0 To WorldRows - 1
                CreateLocation($"({column}, {row})")
            Next
        Next
    End Sub

    Private Function CreateLocation(name As String) As ILocation
        Return Location.Create(_worldData, name)
    End Function
    Private Function CreateCharacter(location As ILocation) As ICharacter
        Return Character.Create(_worldData, location)
    End Function
End Class
