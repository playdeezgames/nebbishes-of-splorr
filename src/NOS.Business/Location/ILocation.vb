Public Interface ILocation
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property Routes As IEnumerable(Of IRoute)
    Function HasRoute(direction As Directions) As Boolean
    ReadOnly Property Route(direction As Directions) As IRoute
End Interface
