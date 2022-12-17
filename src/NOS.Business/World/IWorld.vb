Public Interface IWorld
    ReadOnly Property IsInPlay As Boolean
    Sub Start()
    Function AdvanceTimeWhile(minutes As Integer, conditionCheck As Func(Of Boolean)) As Integer
    Sub Abandon()
    ReadOnly Property PlayerCharacter As ICharacter
    ReadOnly Property Locations As IEnumerable(Of ILocation)
    Sub Save(slot As Integer)
    Sub Load(slot As Integer)
    Property InteractionFeature As IFeature
    Function CreateItem(itemType As ItemTypes) As IItem
End Interface
