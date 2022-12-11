Public Interface IWorld
    ReadOnly Property IsInPlay As Boolean
    Sub Start()
    ReadOnly Property PlayerCharacter As ICharacter
End Interface
