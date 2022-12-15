Public Class Feature
    Implements IFeature
    Private ReadOnly _worldData As WorldData
    ReadOnly Property Id As Integer
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
        Return result
    End Function
End Class
