Friend Class Location
    Implements ILocation
    Private _worldData As WorldData
    Public ReadOnly Property Id As Integer Implements ILocation.Id
    Public ReadOnly Property Name As String Implements ILocation.Name
        Get
            Return _worldData.Locations(Id).Name
        End Get
    End Property
    Public ReadOnly Property Routes As IEnumerable(Of IRoute) Implements ILocation.Routes
        Get
            Return _worldData.Locations(Id).Routes.Select(Function(x) New Route(_worldData, Id, CType(x.Key, Directions)))
        End Get
    End Property
    Public ReadOnly Property Route(direction As Directions) As IRoute Implements ILocation.Route
        Get
            If Not HasRoute(direction) Then
                Return Nothing
            End If
            Return New Route(_worldData, Id, direction)
        End Get
    End Property
    Public ReadOnly Property CanForage As Boolean Implements ILocation.CanForage
        Get
            Return _worldData.Locations(Id).Statistics.ContainsKey(StatisticTypes.ForagingLevel)
        End Get
    End Property
    Private ReadOnly Property LocationType As LocationTypes
        Get
            Return CType(_worldData.Locations(Id).LocationType, LocationTypes)
        End Get
    End Property
    Public ReadOnly Property ForagingLevel As Integer Implements ILocation.ForagingLevel
        Get
            Return GetStatistic(StatisticTypes.ForagingLevel)
        End Get
    End Property
    Private Function GetStatistic(statisticType As StatisticTypes) As Integer
        Return _worldData.Locations(Id).Statistics(statisticType)
    End Function
    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub
    Friend Shared Function Create(worldData As WorldData, name As String, locationType As LocationTypes) As Location
        Dim id = If(worldData.Locations.Any, worldData.Locations.Keys.Max + 1, 0)
        worldData.Locations.Add(id, New LocationData With
                                {
                                    .Name = name,
                                    .LocationType = locationType
                                })
        Dim result = New Location(worldData, id)
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
        Return Item.Create(_worldData, itemType)
    End Function

    Public Sub AddItem(item As IItem) Implements ILocation.AddItem
        _worldData.Locations(Id).ItemIds.Add(item.Id)
    End Sub
End Class
