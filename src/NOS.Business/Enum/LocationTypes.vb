Imports System.Runtime.CompilerServices

Public Enum LocationTypes
    Grass
    Trees
End Enum
Module LocationTypesExtensions
    Friend ReadOnly OverworldLocationTypeGenerator As IReadOnlyDictionary(Of LocationTypes, Integer) =
        New Dictionary(Of LocationTypes, Integer) From
        {
            {LocationTypes.Grass, 1000},
            {LocationTypes.Trees, 500}
        }
    <Extension>
    Function Name(locationType As LocationTypes) As String
        Select Case locationType
            Case LocationTypes.Grass
                Return "grass"
            Case LocationTypes.Trees
                Return "trees"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
