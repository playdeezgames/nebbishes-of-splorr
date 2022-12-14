Friend Class Character
    Inherits Thingie
    Implements ICharacter
    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(_worldData, Me.World, _worldData.Characters(Id).LocationId)
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
    Public Sub New(worldData As WorldData, world As World, id As Integer)
        MyBase.New(worldData, world, id)
    End Sub
    Friend Sub SetAsPlayerCharacter() Implements ICharacter.SetAsPlayerCharacter
        _worldData.PlayerCharacterId = Id
    End Sub
    Friend Shared Function Create(worldData As WorldData, world As World, name As String, location As ILocation, characterType As CharacterTypes) As Character
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
        For Each timer In characterType.StartingTimers
            characterData.Timers(timer.Key) = New Integer() {0, timer.Value}
        Next
        worldData.Characters.Add(id, characterData)
        Return New Character(worldData, world, id)
    End Function
    Public Overridable Function AttemptMove(direction As Directions) As Boolean Implements ICharacter.AttemptMove
        DismissMessages()

        If IsDead Then
            AddMessage($"{Name} too dead to move.")
            Return False
        End If

        If Energy = 0 Then
            AddMessage($"{Name} too tired to move.")
            Return False
        End If

        If Not Location.HasRoute(direction) Then
            AddMessage($"{Name} cannot go {direction.Name}.")
            Return False
        End If

        AddMessage($"{Name} go {direction.Name}.")
        Location = Location.Route(direction).ToLocation
        Return True
    End Function
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
    Public ReadOnly Property IsSleeping As Boolean Implements ICharacter.IsSleeping
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
    Public Overridable Function AttemptSleep() As Boolean Implements ICharacter.AttemptSleep
        DismissMessages()
        If IsDead Then
            AddMessage($"Despite claims to the contrary, {Name} cannot sleep while dead.")
            Return False
        End If
        If IsSleeping Then
            AddMessage($"{Name} is already asleep.")
            Return False
        End If
        SetEffect(Effects.Sleeping)
        SetTimer(TimerTypes.Sleep, 60)
        AddMessage($"{Name} sleeps.")
        Return True
    End Function
    Private Sub SetTimer(timerType As TimerTypes, value As Integer)
        _worldData.Characters(Id).Timers(timerType) = New Integer() {0, value}
    End Sub
    Private Sub ClearEffect(effect As Effects)
        _worldData.Characters(Id).Effects.Remove(effect)
    End Sub
    Public Overrides Sub NextRound()
        If IsDead Then
            Return
        End If
        If Health = 0 Then
            SetEffect(Effects.Dead)
            Return
        End If
        MyBase.NextRound()
        If HasEffect(Effects.Starving) Then
            Wounds += 1
        End If
        If Satiety = 0 Then
            SetEffect(Effects.Starving)
        Else
            ClearEffect(Effects.Starving)
        End If
        If HasEffect(Effects.Foraging) Then
            ForagingXP += 1
            If ForagingXP >= ForagingXPGoal Then
                ForagingXP -= ForagingXPGoal
                ForagingLevel += 1
                AddMessage($"{Name} leveled up foraging!")
            End If
        End If
    End Sub

    Private ReadOnly Property CharacterType As CharacterTypes
        Get
            Return CType(_worldData.Characters(Id).CharacterType, CharacterTypes)
        End Get
    End Property

    Protected Overrides ReadOnly Property Timers As IEnumerable(Of ITimer)
        Get
            Return _worldData.Characters(Id).Timers.Keys.Select(Function(x) New CharacterTimer(_worldData, World, Id, CType(x, TimerTypes)))
        End Get
    End Property


    Private Property ForagingXP As Integer
        Get
            Return GetStatistic(StatisticTypes.ForagingXP)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticTypes.ForagingXP, value)
        End Set
    End Property
    Private ReadOnly Property ForagingXPGoal As Integer
        Get
            'TODO: magic number
            Return (ForagingLevel + 1) * 10
        End Get
    End Property
    Private Property ForagingLevel As Integer
        Get
            Return GetStatistic(StatisticTypes.ForagingLevel)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticTypes.ForagingLevel, value)
        End Set
    End Property
    Public Overridable Function AttemptForage() As Boolean Implements ICharacter.AttemptForage
        DismissMessages()
        If IsDead Then
            AddMessage($"{Name} too dead to forage.")
            Return False
        End If
        If Not Location.CanForage Then
            AddMessage($"{Name} cannot forage here.")
            Return False
        End If
        SetEffect(Effects.Foraging)
        ClearEffect(Effects.Foraging)
        If RNG.FromRange(0, ForagingLevel + Location.ForagingLevel) > ForagingLevel Then
            AddMessage($"{Name} finds nothing.")
            Return True
        End If
        Dim item As IItem = Location.Forage()
        If item Is Nothing Then
            AddMessage($"{Name} finds nothing.")
            Return True
        End If
        AddItem(item)
        AddMessage($"{Name} finds {item.Name}, and now has {ItemTypeCount(item.ItemType)}.")
        Return True
    End Function
    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements ICharacter.Items
        Get
            Return _worldData.Characters(Id).ItemIds.Select(Function(x) New Item(_worldData, World, x))
        End Get
    End Property
    ReadOnly Property ItemTypeCount(itemType As ItemTypes) As Integer
        Get
            Return Items.Where(Function(x) x.ItemType = itemType).Count
        End Get
    End Property
    Public Sub AddItem(item As IItem) Implements ICharacter.AddItem
        _worldData.Characters(Id).ItemIds.Add(item.Id)
    End Sub

    Public Sub AttemptDropItems(itemQuantities As IEnumerable(Of (ItemTypes, Integer))) Implements ICharacter.AttemptDropItems
        DismissMessages()
        Dim total = 0
        For Each itemQuantity In itemQuantities
            Dim itemsToDrop = Items.Where(Function(x) x.ItemType = itemQuantity.Item1).Take(itemQuantity.Item2)
            For Each itemToDrop In itemsToDrop
                DropItem(itemToDrop)
                total += 1
            Next
        Next
        AddMessage($"{Name} drops {total} items.")
    End Sub

    Private Sub DropItem(itemToDrop As IItem)
        RemoveItem(itemToDrop)
        Location.AddItem(itemToDrop)
    End Sub

    Public Sub AttemptTakeItems(itemQuantities As IEnumerable(Of (ItemTypes, Integer))) Implements ICharacter.AttemptTakeItems
        DismissMessages()
        Dim total = 0
        For Each itemQuantity In itemQuantities
            Dim itemsToTake = Location.Items.Where(Function(x) x.ItemType = itemQuantity.Item1).Take(itemQuantity.Item2)
            For Each itemToTake In itemsToTake
                TakeItem(itemToTake)
                total += 1
            Next
        Next
        AddMessage($"{Name} takes {total} items.")
    End Sub

    Private Sub TakeItem(itemToTake As IItem)
        AddItem(itemToTake)
        Location.RemoveItem(itemToTake)
    End Sub

    Public Sub AttemptTakeFeatureItems(feature As IFeature, itemQuantities As IEnumerable(Of (ItemTypes, Integer))) Implements ICharacter.AttemptTakeFeatureItems
        DismissMessages()
        Dim total = 0
        For Each itemQuantity In itemQuantities
            Dim itemsToTake = feature.Items.Where(Function(x) x.ItemType = itemQuantity.Item1).Take(itemQuantity.Item2)
            For Each itemToTake In itemsToTake
                TakeFeatureItem(feature, itemToTake)
                total += 1
            Next
        Next
        AddMessage($"{Name} takes {total} items.")
    End Sub

    Private Sub TakeFeatureItem(feature As IFeature, itemToTake As IItem)
        AddItem(itemToTake)
        feature.RemoveItem(itemToTake)
    End Sub

    Public Sub AttemptEat(itemQuantities As IEnumerable(Of (ItemTypes, Integer))) Implements ICharacter.AttemptEat
        DismissMessages()
        If IsDead Then
            AddMessage($"{Name} has no further need of food.")
            Return
        End If
        Dim total = 0
        For Each itemQuantity In itemQuantities
            Dim itemsToEat = Items.Where(Function(x) x.ItemType = itemQuantity.Item1).Take(itemQuantity.Item2)
            For Each itemToEat In itemsToEat
                EatItem(itemToEat)
                total += 1
            Next
        Next
        AddMessage($"{Name} eats {total} items.")
    End Sub

    Private Sub EatItem(itemToEat As IItem)
        RemoveItem(itemToEat)
        Select Case itemToEat.ItemType
            Case ItemTypes.Berry
                EatBerry(itemToEat)
        End Select
        itemToEat.Destroy()
    End Sub
    Private Sub EatBerry(itemToEat As IItem)
        Hunger -= itemToEat.Satiety
    End Sub

    Public Sub Wake() Implements ICharacter.Wake
        ClearEffect(Effects.Sleeping)
    End Sub

    Public Sub ClearTimer(timerType As TimerTypes) Implements ICharacter.ClearTimer
        _worldData.Characters(Id).Timers.Remove(timerType)
    End Sub

    Public Sub RemoveItem(item As IItem) Implements IItemHolder.RemoveItem
        _worldData.Characters(Id).ItemIds.Remove(item.Id)
    End Sub

    Protected Overrides Sub OnTriggerTimer(timerType As TimerTypes)
        CharacterType.HandleTimer(timerType, Me)
    End Sub

    Public ReadOnly Property MaximumEnergy As Integer Implements ICharacter.MaximumEnergy
        Get
            Return GetStatistic(StatisticTypes.MaximumEnergy)
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

    Public ReadOnly Property HasItems As Boolean Implements IItemHolder.HasItems
        Get
            Return _worldData.Characters(Id).ItemIds.Any
        End Get
    End Property
End Class
