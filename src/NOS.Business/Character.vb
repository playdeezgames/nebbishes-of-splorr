Public Class Character
    Private _worldData As WorldData
    Public ReadOnly Id As Integer
    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Sub SetAsPlayerCharacter()
        _worldData.PlayerCharacterId = Id
    End Sub

    Friend Shared Function Create(worldData As WorldData, location As Location) As Character
        Dim id = worldData.Characters.Count
        worldData.Characters.Add(New CharacterData With {.LocationId = location.Id})
        Return New Character(worldData, id)
    End Function
End Class
