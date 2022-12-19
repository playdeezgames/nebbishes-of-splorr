Public Interface ILocation
    Inherits IThingie
    Inherits IItemHolder
    ReadOnly Property Name As String
    ReadOnly Property Routes As IEnumerable(Of IRoute)
    Function HasRoute(direction As Directions) As Boolean
    Function Forage() As IItem
    Sub NextRound()
    ReadOnly Property Route(direction As Directions) As IRoute
    ReadOnly Property CanForage As Boolean
    ReadOnly Property ForagingLevel As Integer
    ReadOnly Property LocationType As LocationTypes
    ReadOnly Property Features As IEnumerable(Of IFeature)
    ReadOnly Property CanSpawn(characterType As CharacterTypes) As Boolean
End Interface
