Imports System.Runtime.CompilerServices

Public Enum LocationTypes
    Grass
    Trees
    Village
    Cave
    Town
    Dungeon
    Tower
    City
    Farm
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
            }
        }
    <Extension>
    Function StartingStatistics(locationType As LocationTypes) As IReadOnlyDictionary(Of StatisticTypes, Integer)
        Return _startingStatistics(locationType)
    End Function
End Module
