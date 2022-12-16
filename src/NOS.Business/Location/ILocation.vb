Public Interface ILocation
    ReadOnly Property Id As Integer
    ReadOnly Property Name As String
    ReadOnly Property Routes As IEnumerable(Of IRoute)
    Function HasRoute(direction As Directions) As Boolean
    Function Forage() As IItem
    Sub AddItem(item As IItem)
    Sub RemoveItem(item As IItem)
    Sub NextRound()
    ReadOnly Property HasItems As Boolean
    ReadOnly Property Route(direction As Directions) As IRoute
    ReadOnly Property CanForage As Boolean
    ReadOnly Property ForagingLevel As Integer
    ReadOnly Property Items As IEnumerable(Of IItem)
    ReadOnly Property LocationType As LocationTypes
    ReadOnly Property Features As IEnumerable(Of IFeature)
    ReadOnly Property CanSpawn(characterType As CharacterTypes) As Boolean
End Interface
