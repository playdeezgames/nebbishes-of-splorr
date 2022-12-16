Friend Class Location
    Inherits Thingie
    Implements ILocation
    Public ReadOnly Property Name As String Implements ILocation.Name
        Get
            Return _worldData.Locations(Id).Name
        End Get
    End Property
    Public ReadOnly Property Routes As IEnumerable(Of IRoute) Implements ILocation.Routes
        Get
            Return _worldData.Locations(Id).Routes.Select(Function(x) New Route(_worldData, World, Id, CType(x.Key, Directions)))
        End Get
    End Property
    Public ReadOnly Property Route(direction As Directions) As IRoute Implements ILocation.Route
        Get
            If Not HasRoute(direction) Then
                Return Nothing
            End If
            Return New Route(_worldData, World, Id, direction)
        End Get
    End Property
    Public ReadOnly Property CanForage As Boolean Implements ILocation.CanForage
        Get
            Return _worldData.Locations(Id).Statistics.ContainsKey(StatisticTypes.ForagingLevel)
        End Get
    End Property
    Public ReadOnly Property LocationType As LocationTypes Implements ILocation.LocationType
        Get
            Return CType(_worldData.Locations(Id).LocationType, LocationTypes)
        End Get
    End Property
    Public ReadOnly Property ForagingLevel As Integer Implements ILocation.ForagingLevel
        Get
            Return GetStatistic(StatisticTypes.ForagingLevel)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements ILocation.HasItems
        Get
            Return _worldData.Locations(Id).ItemIds.Any
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements ILocation.Items
        Get
            Return _worldData.Locations(Id).ItemIds.Select(Function(x) New Item(_worldData, World, x))
        End Get
    End Property

    Public ReadOnly Property Features As IEnumerable(Of IFeature) Implements ILocation.Features
        Get
            Return _worldData.Locations(Id).FeatureIds.Select(Function(x) New Feature(_worldData, World, x))
        End Get
    End Property

    Public ReadOnly Property CanSpawn(characterType As CharacterTypes) As Boolean Implements ILocation.CanSpawn
        Get
            Return LocationType.CanSpawn(characterType)
        End Get
    End Property

    Private Function GetStatistic(statisticType As StatisticTypes) As Integer
        Return _worldData.Locations(Id).Statistics(statisticType)
    End Function
    Public Sub New(worldData As WorldData, world As IWorld, id As Integer)
        MyBase.New(worldData, world, id)
    End Sub
    Friend Shared Function Create(worldData As WorldData, world As World, name As String, locationType As LocationTypes) As Location
        Dim id = If(worldData.Locations.Any, worldData.Locations.Keys.Max + 1, 0)
        worldData.Locations.Add(id, New LocationData With
                                {
                                    .Name = name,
                                    .LocationType = locationType
                                })
        Dim result = New Location(worldData, world, id)
        For Each statisticType In locationType.StartingStatistics
            result.SetStatistic(statisticType.Key, statisticType.Value)
        Next
        Return result
    End Function
    Private Sub SetStatistic(statisticType As StatisticTypes, value As Integer)
        _worldData.Locations(Id).Statistics(statisticType) = value
    End Sub
    Public Function HasRoute(direction As Directions) As Boolean Implements ILocation.HasRoute
        Return _worldData.Locations(Id).Routes.ContainsKey(direction)
    End Function
    Public Function Forage() As IItem Implements ILocation.Forage
        Dim itemType = RNG.FromGenerator(ForageGenerators(LocationType))
        Return Item.Create(_worldData, World, itemType)
    End Function

    Public Sub AddItem(item As IItem) Implements ILocation.AddItem
        _worldData.Locations(Id).ItemIds.Add(item.Id)
    End Sub

    Public Sub RemoveItem(item As IItem) Implements ILocation.RemoveItem
        _worldData.Locations(Id).ItemIds.Remove(item.Id)
    End Sub

    Public Sub NextRound() Implements ILocation.NextRound
        For Each feature In Features
            feature.NextRound()
        Next
    End Sub
End Class
