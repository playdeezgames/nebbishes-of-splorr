﻿Friend Class Character
    Implements ICharacter
    Private _worldData As WorldData
    Public ReadOnly Id As Integer

    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(_worldData, _worldData.Characters(Id).LocationId)
        End Get
        Set(value As ILocation)
            _worldData.Characters(Id).LocationId = value.Id
        End Set
    End Property

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return _worldData.Characters(Id).Name
        End Get
    End Property

    Public Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Sub SetAsPlayerCharacter() Implements ICharacter.SetAsPlayerCharacter
        _worldData.PlayerCharacterId = Id
    End Sub

    Friend Shared Function Create(worldData As WorldData, name As String, location As ILocation) As Character
        Dim id = If(worldData.Characters.Any, worldData.Characters.Keys.Max + 1, 0)
        worldData.Characters.Add(id, New CharacterData With {.LocationId = location.Id, .Name = name})
        Return New Character(worldData, id)
    End Function

    Public Sub AttemptMove(direction As Directions) Implements ICharacter.AttemptMove
        DismissMessages()
        If Location.HasRoute(direction) Then
            AddMessage($"{Name} go {direction.Name}.")
            Location = Location.Route(direction).ToLocation
        Else
            AddMessage($"{Name} cannot go {direction.Name}.")
        End If
    End Sub

    Public Sub AddMessage(line As String) Implements ICharacter.AddMessage
        If IsPlayerCharacter Then
            _worldData.Messages.Add(line)
        End If
    End Sub

    Public Sub DismissMessages() Implements ICharacter.DismissMessages
        If IsPlayerCharacter AndAlso _worldData.Messages.Any Then
            _worldData.Messages.Clear()
        End If
    End Sub

    Private ReadOnly Property IsPlayerCharacter As Boolean
        Get
            Return _worldData.PlayerCharacterId.HasValue AndAlso _worldData.PlayerCharacterId.Value = Id
        End Get
    End Property

    Public ReadOnly Property HasMessages As Boolean Implements ICharacter.HasMessages
        Get
            Return IsPlayerCharacter AndAlso _worldData.Messages.Any
        End Get
    End Property

    Public ReadOnly Property Messages As String() Implements ICharacter.Messages
        Get
            If IsPlayerCharacter AndAlso _worldData.Messages.Any Then
                Return _worldData.Messages.ToArray
            End If
            Return Array.Empty(Of String)
        End Get
    End Property
End Class
