Public Class World
    Implements IWorld
    Private _worldData As WorldData

    Public ReadOnly Property IsInPlay As Boolean Implements IWorld.IsInPlay
        Get
            Return _worldData IsNot Nothing
        End Get
    End Property

    Public Sub Start() Implements IWorld.Start
        _worldData = New WorldData
        Dim location = CreateLocation()
        Dim character = CreateCharacter(location)
        character.SetAsPlayerCharacter()
    End Sub

    Private Function CreateLocation() As Location
        Return Location.Create(_worldData)
    End Function
    Private Function CreateCharacter(location As Location) As Character
        Return Character.Create(_worldData, location)
    End Function
End Class
