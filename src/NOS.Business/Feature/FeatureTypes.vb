Imports System.Runtime.CompilerServices

Public Enum FeatureTypes
    BerryBush
    FallenLog
    SmallPond
End Enum
Module FeatureTypesExtensions
    <Extension>
    Function Name(featureType As FeatureTypes) As String
        Select Case featureType
            Case FeatureTypes.SmallPond
                Return "small pond"
            Case FeatureTypes.FallenLog
                Return "fallen log"
            Case FeatureTypes.BerryBush
                Return "berry bush"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
