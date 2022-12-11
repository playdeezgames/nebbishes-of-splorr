Public Class WorldData
    Public Property PlayerCharacterId As Integer?
    Public Property Locations As New Dictionary(Of Integer, LocationData)
    Public Property Characters As New List(Of CharacterData)
End Class
