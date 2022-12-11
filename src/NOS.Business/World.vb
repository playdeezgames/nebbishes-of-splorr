Public Class World
    Implements IWorld
    Private _worldData As WorldData

    Public ReadOnly Property IsInPlay As Boolean Implements IWorld.IsInPlay
        Get
            Return _worldData IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property PlayerCharacter As ICharacter Implements IWorld.PlayerCharacter
        Get
            Return New Character(_worldData, _worldData.PlayerCharacterId)
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
        Dim location = CreateLocation("Spawn")
        Dim character = CreateCharacter(location)
        character.SetAsPlayerCharacter()
    End Sub

    Private Function CreateLocation(name As String) As ILocation
        Return Location.Create(_worldData, name)
    End Function
    Private Function CreateCharacter(location As ILocation) As ICharacter
        Return Character.Create(_worldData, location)
    End Function
End Class
