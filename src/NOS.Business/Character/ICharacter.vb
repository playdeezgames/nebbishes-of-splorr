Public Interface ICharacter
    Sub SetAsPlayerCharacter()
    Sub AttemptMove(direction As Directions)
    Property Location As ILocation
    ReadOnly Property Name As String
    Sub AddMessage(line As String)
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessages()
    Sub AttemptSleep()
    Sub NextRound()
    Sub AttemptForage()
    ReadOnly Property Messages As String()
    Property Energy As Integer
    ReadOnly Property MaximumEnergy As Integer
    ReadOnly Property World As IWorld
    Property Satiety As Integer
    ReadOnly Property MaximumSatiety As Integer
    Property Health As Integer
    ReadOnly Property MaximumHealth As Integer
    ReadOnly Property IsDead As Boolean
End Interface
