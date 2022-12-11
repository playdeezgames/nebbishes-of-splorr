Public Interface IWorld
    ReadOnly Property IsInPlay As Boolean
    Sub Start()
    ReadOnly Property PlayerCharacter As ICharacter
    ReadOnly Property Locations As IEnumerable(Of ILocation)
End Interface
