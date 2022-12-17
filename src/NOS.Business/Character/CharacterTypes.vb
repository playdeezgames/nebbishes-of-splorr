﻿Imports System.Runtime.CompilerServices

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
    Private ReadOnly _startingTimers As IReadOnlyDictionary(Of CharacterTypes, IReadOnlyDictionary(Of TimerTypes, Integer)) =
        New Dictionary(Of CharacterTypes, IReadOnlyDictionary(Of TimerTypes, Integer)) From
        {
            {
                CharacterTypes.Nebbish,
                New Dictionary(Of TimerTypes, Integer) From
                {
                    {TimerTypes.Hunger, 10},
                    {TimerTypes.Fatigue, 10}
                }
            }
        }
    <Extension>
    Function StartingTimers(characterType As CharacterTypes) As IReadOnlyDictionary(Of TimerTypes, Integer)
        Return _startingTimers(characterType)
    End Function
    Private ReadOnly _timerHandlers As IReadOnlyDictionary(Of CharacterTypes, IReadOnlyDictionary(Of TimerTypes, Action(Of ICharacter))) =
        New Dictionary(Of CharacterTypes, IReadOnlyDictionary(Of TimerTypes, Action(Of ICharacter))) From
        {
            {
                CharacterTypes.Nebbish,
                New Dictionary(Of TimerTypes, Action(Of ICharacter)) From
                {
                    {TimerTypes.Hunger, AddressOf HandleHungerTimer},
                    {TimerTypes.Fatigue, AddressOf HandleFatigueTimer}
                }
            }
        }

    Private Sub HandleFatigueTimer(character As ICharacter)
    End Sub

    Private Sub HandleHungerTimer(character As ICharacter)
    End Sub

    <Extension>
    Sub HandleTimer(characterType As CharacterTypes, timerType As TimerTypes, character As ICharacter)
        _timerHandlers(characterType)(timerType)(character)
    End Sub
End Module
