Public Class World
    Implements IWorld
    Private _worldData As WorldData
    Private _random As New Random
    Sub New()
    End Sub
    Sub New(worldData As WorldData)
        _worldData = worldData
    End Sub
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
        CreateCaves()
        CreateFeatures()
    End Sub

    Private Sub CreateFeatures()
        For Each location In Locations
            Dim featureTypes = location.LocationType.GenerateFeatures()
            For Each featureType In featureTypes
                CreateFeature(featureType, location)
            Next
        Next
    End Sub

    Private Function CreateFeature(featureType As FeatureTypes, location As ILocation) As IFeature
        Return Feature.Create(_worldData, featureType, location)
    End Function

    Private Sub CreateCaves()
        Dim caveLocations = Locations.Where(Function(x) x.LocationType = LocationTypes.Cave)
        For Each caveLocation In caveLocations
            CreateCave(caveLocation)
        Next
    End Sub
    Private Const CaveColumns = 4
    Private Const CaveRows = 4
    Private Shared ReadOnly mazeDirections As IReadOnlyDictionary(Of Directions, MazeDirection(Of Directions)) =
        New Dictionary(Of Directions, MazeDirection(Of Directions)) From
        {
            {Directions.North, New MazeDirection(Of Directions)(Directions.South, 0, -1)},
            {Directions.East, New MazeDirection(Of Directions)(Directions.West, 1, 0)},
            {Directions.South, New MazeDirection(Of Directions)(Directions.North, 0, 1)},
            {Directions.West, New MazeDirection(Of Directions)(Directions.East, -1, 0)}
        }
    Private Sub CreateCave(caveLocation As ILocation)
        Dim maze = New Maze(Of Directions)(CaveColumns, CaveRows, mazeDirections)
        maze.Generate()
        Dim tunnels(CaveColumns - 1, CaveRows - 1) As ILocation
        For column = 0 To CaveColumns - 1
            For row = 0 To CaveRows - 1
                tunnels(column, row) = CreateLocation(LocationTypes.Tunnel)
            Next
        Next
        For column = 0 To CaveColumns - 1
            For row = 0 To CaveRows - 1
                For Each mazeDirection In mazeDirections
                    Dim door = maze.GetCell(column, row).GetDoor(mazeDirection.Key)
                    If door IsNot Nothing AndAlso door.Open Then
                        CreateRoute(tunnels(column, row), mazeDirection.Key, tunnels(column + CInt(mazeDirection.Value.DeltaX), row + CInt(mazeDirection.Value.DeltaY)))
                    End If
                Next
            Next
        Next
        Dim x = RNG.FromRange(0, CaveColumns - 1)
        Dim y = RNG.FromRange(0, CaveRows - 1)
        CreateRoute(caveLocation, Directions.Down, tunnels(x, y))
        CreateRoute(tunnels(x, y), Directions.up, caveLocation)
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim candidates = Locations.ToList()
        Dim character = CreateCharacter("Tagon", candidates(_random.Next(candidates.Count)), CharacterTypes.Nebbish)
        character.SetAsPlayerCharacter()
    End Sub

    Private Const WorldColumns = 32
    Private Const WorldRows = 32
    Private Sub CreateOverworld()
        Dim overworld As New Dictionary(Of (Integer, Integer), ILocation)
        For column = 0 To WorldColumns - 1
            For row = 0 To WorldRows - 1
                overworld.Add((column, row), CreateLocation(RNG.FromGenerator(OverworldLocationTypeGenerator)))
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

    Private Function CreateLocation(locationType As LocationTypes) As ILocation
        Return Location.Create(_worldData, locationType.Name, locationType)
    End Function
    Private Function CreateCharacter(name As String, location As ILocation, characterType As CharacterTypes) As ICharacter
        Return Character.Create(_worldData, name, location, characterType)
    End Function

    Public Function AdvanceTime(minutes As Integer, conditionCheck As Func(Of Boolean)) As Integer Implements IWorld.AdvanceTime
        Dim counter = 0
        While minutes > 0 AndAlso conditionCheck()
            minutes -= 1
            counter += 1
            NextRound()
        End While
        Return counter
    End Function
    Private ReadOnly Property Characters As IEnumerable(Of ICharacter)
        Get
            Return _worldData.Characters.Keys.Select(Function(x) New Character(_worldData, x))
        End Get
    End Property
    Private _interactionFeatureId As Integer?
    Public Property InteractionFeature As IFeature Implements IWorld.InteractionFeature
        Get
            Return If(_interactionFeatureId.HasValue, New Feature(_worldData, _interactionFeatureId.Value), Nothing)
        End Get
        Set(value As IFeature)
            If value Is Nothing Then
                _interactionFeatureId = Nothing
                Return
            End If
            _interactionFeatureId = value.Id
        End Set
    End Property

    Private Sub NextRound()
        For Each character In Characters
            character.NextRound()
        Next
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
        _worldData = Nothing
    End Sub

    Public Sub Save(slot As Integer) Implements IWorld.Save
        File.WriteAllText($"Slot{slot}.json", JsonSerializer.Serialize(_worldData))
    End Sub

    Public Sub Load(slot As Integer) Implements IWorld.Load
        _worldData = JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText($"Slot{slot}.json"))
    End Sub
End Class
