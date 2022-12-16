Imports System.Runtime.CompilerServices
Public Enum LocationTypes
    Grass
    Trees
    Village
    Cave
    Tunnel
End Enum
Module LocationTypesExtensions
    Friend ReadOnly OverworldLocationTypeGenerator As IReadOnlyDictionary(Of LocationTypes, Integer) =
        New Dictionary(Of LocationTypes, Integer) From
        {
            {LocationTypes.Grass, 1000},
            {LocationTypes.Trees, 500},
            {LocationTypes.Village, 25},
            {LocationTypes.Cave, 50}
        }
    Friend ReadOnly ForageGenerators As IReadOnlyDictionary(Of LocationTypes, IReadOnlyDictionary(Of ItemTypes, Integer)) =
        New Dictionary(Of LocationTypes, IReadOnlyDictionary(Of ItemTypes, Integer)) From
        {
            {
                LocationTypes.Grass,
                New Dictionary(Of ItemTypes, Integer) From
                {
                    {ItemTypes.PlantFiber, 100}
                }
            },
            {
                LocationTypes.Trees,
                New Dictionary(Of ItemTypes, Integer) From
                {
                    {ItemTypes.Stick, 100},
                    {ItemTypes.PlantFiber, 100}
                }
            }
        }
    <Extension>
    Function Name(locationType As LocationTypes) As String
        Select Case locationType
            Case LocationTypes.Cave
                Return "cave"
            Case LocationTypes.Grass
                Return "grass"
            Case LocationTypes.Trees
                Return "trees"
            Case LocationTypes.Village
                Return "village"
            Case LocationTypes.Tunnel
                Return "tunnel"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private ReadOnly _startingStatistics As IReadOnlyDictionary(Of LocationTypes, IReadOnlyDictionary(Of StatisticTypes, Integer)) =
        New Dictionary(Of LocationTypes, IReadOnlyDictionary(Of StatisticTypes, Integer)) From
        {
            {
                LocationTypes.Grass,
                New Dictionary(Of StatisticTypes, Integer) From
                {
                    {StatisticTypes.ForagingLevel, 10}
                }
            },
            {
                LocationTypes.Trees,
                New Dictionary(Of StatisticTypes, Integer) From
                {
                    {StatisticTypes.ForagingLevel, 10}
                }
            },
            {
                LocationTypes.Cave,
                New Dictionary(Of StatisticTypes, Integer)
            },
            {
                LocationTypes.Village,
                New Dictionary(Of StatisticTypes, Integer)
            },
            {
                LocationTypes.Tunnel,
                New Dictionary(Of StatisticTypes, Integer)
            }
        }
    <Extension>
    Function StartingStatistics(locationType As LocationTypes) As IReadOnlyDictionary(Of StatisticTypes, Integer)
        If Not _startingStatistics.ContainsKey(locationType) Then
            Return New Dictionary(Of StatisticTypes, Integer)
        End If
        Return _startingStatistics(locationType)
    End Function
    Private ReadOnly _featureGenerators As IReadOnlyDictionary(Of LocationTypes, IReadOnlyDictionary(Of FeatureTypes, Double)) =
        New Dictionary(Of LocationTypes, IReadOnlyDictionary(Of FeatureTypes, Double)) From
        {
            {
                LocationTypes.Grass,
                New Dictionary(Of FeatureTypes, Double) From
                {
                    {FeatureTypes.BerryBush, 0.1}
                }
            },
            {
                LocationTypes.Trees,
                New Dictionary(Of FeatureTypes, Double) From
                {
                    {FeatureTypes.FallenLog, 0.1},
                    {FeatureTypes.SmallPond, 0.1}
                }
            }
        }
    <Extension>
    Function GenerateFeatures(locationType As LocationTypes) As IEnumerable(Of FeatureTypes)
        If Not _featureGenerators.ContainsKey(locationType) Then
            Return Array.Empty(Of FeatureTypes)
        End If
        Dim result As New List(Of FeatureTypes)
        For Each generator In _featureGenerators(locationType)
            If RNG.NextDouble < generator.Value Then
                result.Add(generator.Key)
            End If
        Next
        Return result
    End Function
    Private ReadOnly _spawnLocationTypeCharacterTypes As New HashSet(Of (LocationTypes, CharacterTypes)) From
        {
            (LocationTypes.Trees, CharacterTypes.Nebbish),
            (LocationTypes.Grass, CharacterTypes.Nebbish)
        }
    <Extension>
    Function CanSpawn(locationType As LocationTypes, characterType As CharacterTypes) As Boolean
        Return _spawnLocationTypeCharacterTypes.Contains((locationType, characterType))
    End Function
End Module
