Imports System.Runtime.CompilerServices

Public Enum FeatureTypes
    BerryBush
    FallenLog
    SmallPond
End Enum
Module FeatureTypesExtensions
    Private ReadOnly _names As IReadOnlyDictionary(Of FeatureTypes, String) =
        New Dictionary(Of FeatureTypes, String) From
        {
            {FeatureTypes.SmallPond, "small pond"},
            {FeatureTypes.FallenLog, "fallen log"},
            {FeatureTypes.BerryBush, "berry bush"}
        }
    <Extension>
    Function Name(featureType As FeatureTypes) As String
        Return _names(featureType)
    End Function
    Private ReadOnly _populators As IReadOnlyDictionary(Of FeatureTypes, Action(Of IFeature)) =
        New Dictionary(Of FeatureTypes, Action(Of IFeature)) From
        {
            {FeatureTypes.SmallPond, AddressOf PopulateSmallPond},
            {FeatureTypes.FallenLog, AddressOf PopulateFallenLog},
            {FeatureTypes.BerryBush, AddressOf PopulateBerryBush}
        }
    <Extension>
    Sub Populate(featureType As FeatureTypes, feature As IFeature)
        _populators(featureType)(feature)
    End Sub

    Private Sub PopulateSmallPond(feature As IFeature)
        'TODO: add fish
    End Sub

    Private Sub PopulateFallenLog(feature As IFeature)
        'TODO: add grubs
    End Sub

    Private Sub PopulateBerryBush(feature As IFeature)
        Dim berryCount = RNG.RollDice("2d4")
        While berryCount > 0
            berryCount -= 1
            feature.AddItem(feature.World.CreateItem(ItemTypes.Berry))
        End While
    End Sub
End Module
