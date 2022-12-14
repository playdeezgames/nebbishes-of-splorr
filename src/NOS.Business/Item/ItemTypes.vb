Imports System.Runtime.CompilerServices

Public Enum ItemTypes
    PlantFiber
    Stick
End Enum
Public Module ItemTypesExtensions
    <Extension>
    Public Function Name(itemType As ItemTypes) As String
        Select Case itemType
            Case ItemTypes.PlantFiber
                Return "plant fiber"
            Case ItemTypes.Stick
                Return "stick"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module