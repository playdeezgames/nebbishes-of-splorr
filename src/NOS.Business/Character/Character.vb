Friend Class Character
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
    Friend Shared Function Create(worldData As WorldData, name As String, location As ILocation, characterType As CharacterTypes) As Character
        Dim id = If(worldData.Characters.Any, worldData.Characters.Keys.Max + 1, 0)
        Dim characterData = New CharacterData With
            {
                .LocationId = location.Id,
                .Name = name,
                .CharacterType = characterType
            }
        For Each statistic In characterType.StartingStatistics
            characterData.Statistics(statistic.Key) = statistic.Value
        Next
        worldData.Characters.Add(id, characterData)
        Return New Character(worldData, id)
    End Function
    Public Sub AttemptMove(direction As Directions) Implements ICharacter.AttemptMove
        DismissMessages()

        If IsDead Then
            AddMessage($"{Name} too dead to move.")
            Return
        End If

        If Energy = 0 Then
            AddMessage($"{Name} too tired to move.")
            Return
        End If

        If Not Location.HasRoute(direction) Then
            AddMessage($"{Name} cannot go {direction.Name}.")
            Return
        End If

        AddMessage($"{Name} go {direction.Name}.")
        Location = Location.Route(direction).ToLocation
        NextRound()
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
    Public Property Energy As Integer Implements ICharacter.Energy
        Get
            Return MaximumEnergy - Fatigue
        End Get
        Set(value As Integer)
            Fatigue = MaximumEnergy - value
        End Set
    End Property
    Private Property Fatigue As Integer
        Get
            Return GetStatistic(StatisticTypes.Fatigue)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticTypes.Fatigue, Math.Clamp(value, 0, MaximumEnergy))
        End Set
    End Property
    Private Sub SetStatistic(statisticType As StatisticTypes, value As Integer)
        _worldData.Characters(Id).Statistics(statisticType) = value
    End Sub
    Private Function GetStatistic(statisticType As StatisticTypes) As Integer
        Return _worldData.Characters(Id).Statistics(statisticType)
    End Function
    Private ReadOnly Property IsSleeping As Boolean
        Get
            Return HasEffect(Effects.Sleeping)
        End Get
    End Property
    Private Function HasEffect(effect As Effects) As Boolean
        Return _worldData.Characters(Id).Effects.Contains(effect)
    End Function
    Private Sub SetEffect(effect As Effects)
        _worldData.Characters(Id).Effects.Add(effect)
    End Sub
    Public Sub AttemptSleep() Implements ICharacter.AttemptSleep
        DismissMessages()

        If IsDead Then
            AddMessage($"Despite claims to the contrary, {Name} cannot sleep while dead.")
            Return
        End If
        Dim oldEnergy = Energy
        SetEffect(Effects.Sleeping)
        Dim minutes = World.AdvanceTime(60, Function() Me.IsSleeping)
        ClearEffect(Effects.Sleeping)
        AddMessage($"{Name} sleep for {minutes} minutes, +{Energy - oldEnergy} energy.")
    End Sub
    Private Sub ClearEffect(effect As Effects)
        _worldData.Characters(Id).Effects.Remove(effect)
    End Sub
    Public Sub NextRound() Implements ICharacter.NextRound
        If IsDead Then
            Return
        End If
        If Health = 0 Then
            SetEffect(Effects.Dead)
            Return
        End If
        If IsSleeping Then
            Energy += 3
        Else
            Fatigue += 1
        End If
        If HasEffect(Effects.Starving) Then
            Wounds += 1
        End If
        If Satiety = 0 Then
            SetEffect(Effects.Starving)
        Else
            ClearEffect(Effects.Starving)
        End If
        Hunger += 1
    End Sub
    Public Sub AttemptForage() Implements ICharacter.AttemptForage
        DismissMessages()
        If IsDead Then
            AddMessage($"{Name} too dead to forage.")
            Return
        End If
        If Not Location.CanForage Then
            AddMessage($"{Name} cannot forage here.")
            Return
        End If
        NextRound()
        Dim item As IItem = Location.Forage()
        If item Is Nothing Then
            AddMessage($"{Name} finds nothing.")
            Return
        End If
        AddItem(item)
        AddMessage($"{Name} finds {item.Name}, and now has {ItemTypeCount(item.ItemType)}.")
    End Sub
    ReadOnly Property Items As IEnumerable(Of IItem)
        Get
            Return _worldData.Characters(Id).ItemIds.Select(Function(x) New Item(_worldData, x))
        End Get
    End Property
    ReadOnly Property ItemTypeCount(itemType As ItemTypes) As Integer
        Get
            Return Items.Where(Function(x) x.ItemType = itemType).Count
        End Get
    End Property
    Private Sub AddItem(item As IItem)
        _worldData.Characters(Id).ItemIds.Add(item.Id)
    End Sub
    Public ReadOnly Property MaximumEnergy As Integer Implements ICharacter.MaximumEnergy
        Get
            Return GetStatistic(StatisticTypes.MaximumEnergy)
        End Get
    End Property
    Public ReadOnly Property World As IWorld Implements ICharacter.World
        Get
            Return New World(_worldData)
        End Get
    End Property
    Public Property Satiety As Integer Implements ICharacter.Satiety
        Get
            Return MaximumSatiety - Hunger
        End Get
        Set(value As Integer)
            Hunger = MaximumSatiety - value
        End Set
    End Property
    Private Property Hunger As Integer
        Get
            Return GetStatistic(StatisticTypes.Hunger)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticTypes.Hunger, Math.Clamp(value, 0, MaximumSatiety))
        End Set
    End Property
    Public ReadOnly Property MaximumSatiety As Integer Implements ICharacter.MaximumSatiety
        Get
            Return GetStatistic(StatisticTypes.MaximumSatiety)
        End Get
    End Property
    Public Property Health As Integer Implements ICharacter.Health
        Get
            Return MaximumHealth - Wounds
        End Get
        Set(value As Integer)
            Wounds = MaximumHealth - value
        End Set
    End Property
    Private Property Wounds As Integer
        Get
            Return GetStatistic(StatisticTypes.Wounds)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticTypes.Wounds, Math.Clamp(value, 0, MaximumHealth))
        End Set
    End Property
    Public ReadOnly Property MaximumHealth As Integer Implements ICharacter.MaximumHealth
        Get
            Return GetStatistic(StatisticTypes.MaximumHealth)
        End Get
    End Property
    Public ReadOnly Property IsDead As Boolean Implements ICharacter.IsDead
        Get
            Return HasEffect(Effects.Dead)
        End Get
    End Property
End Class
