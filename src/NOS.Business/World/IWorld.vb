Public Interface IWorld
    ReadOnly Property IsInPlay As Boolean
    Sub Start()
    Function AdvanceTime(minutes As Integer, conditionCheck As Func(Of Boolean)) As Integer
    Sub Abandon()
    ReadOnly Property PlayerCharacter As ICharacter
    ReadOnly Property Locations As IEnumerable(Of ILocation)
End Interface
