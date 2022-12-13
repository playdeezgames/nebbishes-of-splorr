Imports System.Runtime.CompilerServices

Public Enum CharacterTypes
    Nebbish
End Enum
Module CharacterTypesExtensions
    Private ReadOnly _startingStatistics As IReadOnlyDictionary(Of CharacterTypes, IReadOnlyDictionary(Of StatisticTypes, Integer)) =
        New Dictionary(Of CharacterTypes, IReadOnlyDictionary(Of StatisticTypes, Integer)) From
        {
            {
                CharacterTypes.Nebbish,
                New Dictionary(Of StatisticTypes, Integer) From
                {
                    {StatisticTypes.Fatigue, 0},
                    {StatisticTypes.MaximumEnergy, 1000},
                    {StatisticTypes.Hunger, 0},
                    {StatisticTypes.MaximumSatiety, 1000},
                    {StatisticTypes.Wounds, 0},
                    {StatisticTypes.MaximumHealth, 1000},
                    {StatisticTypes.ForagingXP, 0},
                    {StatisticTypes.ForagingLevel, 10}
                }
            }
        }
    <Extension>
    Function StartingStatistics(characterType As CharacterTypes) As IReadOnlyDictionary(Of StatisticTypes, Integer)
        Return _startingStatistics(characterType)
    End Function
End Module
