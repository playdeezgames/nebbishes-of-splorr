Public Interface IWorld
    ReadOnly Property IsInPlay As Boolean
    Sub Start()
    Sub Abandon()
    ReadOnly Property PlayerCharacter As ICharacter
    ReadOnly Property Locations As IEnumerable(Of ILocation)
    Sub Save(slot As Integer)
    Sub Load(slot As Integer)
    Property InteractionFeature As IFeature
    Function CreateItem(itemType As ItemTypes) As IItem
    Sub NextRound()
End Interface
