Public Interface IFeature
    Inherits IThingie
    Inherits IItemHolder
    ReadOnly Property Name As String
    ReadOnly Property FeatureType As FeatureTypes
End Interface
