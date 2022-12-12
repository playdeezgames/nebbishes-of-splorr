Public Interface ICharacter
    Sub SetAsPlayerCharacter()
    Sub AttemptMove(direction As Directions)
    Property Location As ILocation
    ReadOnly Property Name As String
    Sub AddMessage(ParamArray lines As String())
End Interface
