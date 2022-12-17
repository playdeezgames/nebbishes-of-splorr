Public Interface IItem
    Inherits IThingie
    ReadOnly Property Name As String
    ReadOnly Property ItemType As ItemTypes
    ReadOnly Property CanEat As Boolean
    Sub Destroy()
    Sub NextRound()
    ReadOnly Property Satiety As Integer
End Interface
