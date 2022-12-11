Friend Class Character
    Implements ICharacter
    Private _worldData As WorldData
    Public ReadOnly Id As Integer

    Public ReadOnly Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(_worldData, _worldData.Characters(Id).LocationId)
        End Get
    End Property

    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Sub SetAsPlayerCharacter() Implements ICharacter.SetAsPlayerCharacter
        _worldData.PlayerCharacterId = Id
    End Sub

    Friend Shared Function Create(worldData As WorldData, location As ILocation) As Character
        Dim id = worldData.Characters.Count
        worldData.Characters.Add(New CharacterData With {.LocationId = location.Id})
        Return New Character(worldData, id)
    End Function
End Class
