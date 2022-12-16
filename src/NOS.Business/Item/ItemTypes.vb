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
End Module