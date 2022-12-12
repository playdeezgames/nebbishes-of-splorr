Public Interface ICharacter
    Sub SetAsPlayerCharacter()
    Sub AttemptMove(direction As Directions)
    Property Location As ILocation
    ReadOnly Property Name As String
    Sub AddMessage(ParamArray lines As String())
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessage()
    ReadOnly Property CurrentMessage As String()
End Interface
