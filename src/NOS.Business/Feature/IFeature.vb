Public Interface IFeature
    Inherits IThingie
    ReadOnly Property Name As String
    ReadOnly Property FeatureType As FeatureTypes
    Sub NextRound()
    Sub AddItem(item As IItem)
    Sub RemoveItem(item As IItem)
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
