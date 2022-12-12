Imports System.Runtime.CompilerServices

Public Enum Directions
    North
    East
    South
    West
End Enum
Public Module DirectionsExtensions
    <Extension>
    Function Letter(direction As Directions) As String
        Select Case direction
            Case Directions.North
                Return "N"
            Case Directions.East
                Return "E"
            Case Directions.South
                Return "S"
            Case Directions.West
                Return "W"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function Name(direction As Directions) As String
        Select Case direction
            Case Directions.North
                Return "north"
            Case Directions.East
                Return "east"
            Case Directions.South
                Return "south"
            Case Directions.West
                Return "west"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
