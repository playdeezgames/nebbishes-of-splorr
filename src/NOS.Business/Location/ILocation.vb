Public Interface ILocation
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property Routes As IEnumerable(Of IRoute)
    Function HasRoute(direction As Directions) As Boolean
    Function Forage() As IItem
    Sub AddItem(item As IItem)
    ReadOnly Property Route(direction As Directions) As IRoute
    ReadOnly Property CanForage As Boolean
    ReadOnly Property ForagingLevel As Integer
End Interface
