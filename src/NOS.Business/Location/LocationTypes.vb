Imports System.Runtime.CompilerServices

Public Enum LocationTypes
    Grass
    Trees
    Village
    Lair
End Enum
Module LocationTypesExtensions
    Friend ReadOnly OverworldLocationTypeGenerator As IReadOnlyDictionary(Of LocationTypes, Integer) =
        New Dictionary(Of LocationTypes, Integer) From
        {
            {LocationTypes.Grass, 1000},
            {LocationTypes.Trees, 500},
            {LocationTypes.Village, 25},
            {LocationTypes.Lair, 50}
        }
    Friend ReadOnly ForageGenerators As IReadOnlyDictionary(Of LocationTypes, IReadOnlyDictionary(Of ItemTypes, Integer)) =
        New Dictionary(Of LocationTypes, IReadOnlyDictionary(Of ItemTypes, Integer)) From
        {
            {
                LocationTypes.Grass,
                New Dictionary(Of ItemTypes, Integer) From
                {
                    {ItemTypes.None, 100},
                    {ItemTypes.PlantFiber, 100}
                }
            },
            {
                LocationTypes.Trees,
                New Dictionary(Of ItemTypes, Integer) From
                {
                    {ItemTypes.None, 100},
                    {ItemTypes.Stick, 100},
                    {ItemTypes.PlantFiber, 100}
                }
            }
        }
    <Extension>
    Function Name(locationType As LocationTypes) As String
        Select Case locationType
            Case LocationTypes.Grass
                Return "grass"
            Case LocationTypes.Trees
                Return "trees"
            Case LocationTypes.Village
                Return "village"
            Case LocationTypes.Lair
                Return "lair"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function CanForage(locationType As LocationTypes) As Boolean
        Select Case locationType
            Case LocationTypes.Grass, LocationTypes.Trees
                Return True
            Case LocationTypes.Village, LocationTypes.Lair
                Return False
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
