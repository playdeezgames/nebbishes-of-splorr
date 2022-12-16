Imports System.Runtime.CompilerServices

Public Enum FeatureTypes
    BerryBush
    FallenLog
    SmallPond
End Enum
Module FeatureTypesExtensions
    <Extension>
    Function Name(featureType As FeatureTypes) As String
        Select Case featureType
            Case FeatureTypes.SmallPond
                Return "small pond"
            Case FeatureTypes.FallenLog
                Return "fallen log"
            Case FeatureTypes.BerryBush
                Return "berry bush"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Sub Populate(featureType As FeatureTypes, feature As IFeature)
        Select Case featureType
            Case FeatureTypes.BerryBush
                PopulateBerryBush(feature)
            Case FeatureTypes.FallenLog
                PopulateFallenLog(feature)
            Case FeatureTypes.SmallPond
                PopulateSmallPond(feature)
        End Select
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
