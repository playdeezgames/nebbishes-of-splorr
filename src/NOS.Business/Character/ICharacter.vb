Public Interface ICharacter
    Inherits IThingie
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
    Sub AttemptDropItems(itemQuantities As IEnumerable(Of (ItemTypes, Integer)))
    Sub AttemptTakeItems(itemQuantities As IEnumerable(Of (ItemTypes, Integer)))
    Sub AttemptTakeFeatureItems(feature As IFeature, itemQuantities As IEnumerable(Of (ItemTypes, Integer)))
    Sub AttemptEat(transferQuantities As IEnumerable(Of (ItemTypes, Integer)))
    ReadOnly Property Messages As String()
    Property Energy As Integer
    ReadOnly Property MaximumEnergy As Integer
    Property Satiety As Integer
    ReadOnly Property MaximumSatiety As Integer
    Property Health As Integer
    ReadOnly Property MaximumHealth As Integer
    ReadOnly Property IsDead As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
