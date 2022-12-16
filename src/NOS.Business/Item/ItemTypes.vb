Imports System.Runtime.CompilerServices

Public Enum ItemTypes
    PlantFiber
    Stick
    Berry
End Enum
Public Module ItemTypesExtensions
    <Extension>
    Public Function Name(itemType As ItemTypes) As String
        Select Case itemType
            Case ItemTypes.PlantFiber
                Return "plant fiber"
            Case ItemTypes.Stick
                Return "stick"
            Case ItemTypes.Berry
                Return "berry"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Public Function CanEat(itemType As ItemTypes) As Boolean
        Select Case itemType
            Case ItemTypes.PlantFiber
                Return False
            Case ItemTypes.Stick
                Return False
            Case ItemTypes.Berry
                Return True
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Public Function Satiety(itemType As ItemTypes) As Integer
        Select Case itemType
            Case ItemTypes.PlantFiber
                Return 0
            Case ItemTypes.Stick
                Return 0
            Case ItemTypes.Berry
                Return 10
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module