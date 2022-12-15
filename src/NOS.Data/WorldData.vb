Public Class WorldData
    Public Property PlayerCharacterId As Integer?
    Public Property Locations As New Dictionary(Of Integer, LocationData)
    Public Property Characters As New Dictionary(Of Integer, CharacterData)
    Public Property Items As New Dictionary(Of Integer, ItemData)
    Public Property Messages As New List(Of String)
    Public Property Features As New Dictionary(Of Integer, FeatureData)
End Class
