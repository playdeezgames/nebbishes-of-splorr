Public Interface ICharacter
    Sub SetAsPlayerCharacter()
    Sub AttemptMove(direction As Directions)
    Property Location As ILocation
    ReadOnly Property Name As String
    Sub AddMessage(line As String)
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessages()
    Sub AttemptSleep()
    ReadOnly Property Messages As String()
    Property Energy As Integer
    ReadOnly Property MaximumEnergy As Integer
End Interface
