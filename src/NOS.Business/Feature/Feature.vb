Public Class Feature
    Inherits Thingie
    Implements IFeature
    Public ReadOnly Property Name As String Implements IFeature.Name
        Get
            Return FeatureType.Name
        End Get
    End Property
    Public ReadOnly Property FeatureType As FeatureTypes Implements IFeature.FeatureType
        Get
            Return CType(_worldData.Features(Id).FeatureType, FeatureTypes)
        End Get
    End Property
    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IFeature.Items
        Get
            Return _worldData.Features(Id).ItemIds.Select(Function(x) New Item(_worldData, World, x))
        End Get
    End Property
    Sub New(worldData As WorldData, world As IWorld, id As Integer)
        MyBase.New(worldData, world, id)
    End Sub
    Friend Shared Function Create(worldData As WorldData, world As World, featureType As FeatureTypes, location As ILocation) As IFeature
        Dim id = If(worldData.Features.Any, worldData.Features.Keys.Max + 1, 0)
        worldData.Features.Add(id, New FeatureData With
                                {
                                    .FeatureType = featureType
                                })
        Dim result = New Feature(worldData, world, id)
        worldData.Locations(location.Id).FeatureIds.Add(id)
        featureType.Populate(result)
        Return result
    End Function
    Public Overrides Sub NextRound()
        AdvanceTimers()
    End Sub
    Private Sub AdvanceTimers()
        For Each timer In Timers

        Next
    End Sub
    Protected Overrides ReadOnly Property Timers As IEnumerable(Of ITimer)
        Get
            Return _worldData.Features(Id).Timers.Keys.Select(Function(x) New FeatureTimer(_worldData, World, Id, CType(x, TimerTypes)))
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements IItemHolder.HasItems
        Get
            Return _worldData.Features(Id).ItemIds.Any
        End Get
    End Property

    Public Sub AddItem(item As IItem) Implements IFeature.AddItem
        _worldData.Features(Id).ItemIds.Add(item.Id)
    End Sub
    Public Sub RemoveItem(item As IItem) Implements IFeature.RemoveItem
        _worldData.Features(Id).ItemIds.Remove(item.Id)
    End Sub

    Protected Overrides Sub OnTriggerTimer(timerType As TimerTypes)
        'TODO: feature type based effect
    End Sub
End Class
