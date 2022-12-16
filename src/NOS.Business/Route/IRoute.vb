Public Interface IRoute
    Inherits IBaseThingie
    ReadOnly Property LocationId As Integer
    ReadOnly Property Direction As Directions
    ReadOnly Property FromLocation As ILocation
    ReadOnly Property ToLocation As ILocation
End Interface
