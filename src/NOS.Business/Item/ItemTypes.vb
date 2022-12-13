Imports System.Runtime.CompilerServices

Public Enum ItemTypes
    None
    PlantFiber
    Stick
End Enum
Module ItemTypesExtensions
    <Extension>
    Function Name(itemType As ItemTypes) As String
        Select Case itemType
            Case ItemTypes.None
                Return "nothing"
            Case ItemTypes.PlantFiber
                Return "plant fiber"
            Case ItemTypes.Stick
                Return "stick"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module