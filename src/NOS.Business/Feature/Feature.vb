Public Class Feature
    Implements IFeature
    Private ReadOnly _worldData As WorldData
    ReadOnly Property Id As Integer Implements IFeature.Id

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

    Public ReadOnly Property World As IWorld Implements IFeature.World
        Get
            Return New World(_worldData)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IFeature.Items
        Get
            Return _worldData.Features(Id).ItemIds.Select(Function(x) New Item(_worldData, x))
        End Get
    End Property

    Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Shared Function Create(worldData As WorldData, featureType As FeatureTypes, location As ILocation) As IFeature
        Dim id = If(worldData.Features.Any, worldData.Features.Keys.Max + 1, 0)
        worldData.Features.Add(id, New FeatureData With
                                {
                                    .FeatureType = featureType
                                })
        Dim result = New Feature(worldData, id)
        worldData.Locations(location.Id).FeatureIds.Add(id)
        featureType.Populate(result)
        Return result
    End Function

    Public Sub NextRound() Implements IFeature.NextRound
        'TODO: whatever the feature does, prolly based on the feature type
    End Sub

    Public Sub AddItem(item As IItem) Implements IFeature.AddItem
        _worldData.Features(Id).ItemIds.Add(item.Id)
    End Sub

    Public Sub RemoveItem(item As IItem) Implements IFeature.RemoveItem
        _worldData.Features(Id).ItemIds.Remove(item.Id)
    End Sub
End Class
