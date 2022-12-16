Public Interface IFeature
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property FeatureType As FeatureTypes
    Sub NextRound()
    ReadOnly Property World As IWorld
    Sub AddItem(item As IItem)
    Sub RemoveItem(item As IItem)
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
