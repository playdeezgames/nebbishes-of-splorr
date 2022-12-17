Public Class Item
    Inherits Thingie
    Implements IItem
    Sub New(worldData As WorldData, world As IWorld, id As Integer)
        MyBase.New(worldData, world, id)
    End Sub

    Friend Shared Function Create(worldData As WorldData, world As IWorld, itemType As ItemTypes) As IItem
        Dim id = If(worldData.Items.Any, worldData.Items.Keys.Max + 1, 0)
        worldData.Items.Add(id, New ItemData With
                                {
                                    .ItemType = itemType
                                })
        Return New Item(worldData, world, id)
    End Function

    Public Sub Destroy() Implements IItem.Destroy
        _worldData.Items.Remove(Id)
    End Sub

    Public Sub NextRound() Implements IItem.NextRound
        'TODO: things!
    End Sub

    Public ReadOnly Property Name As String Implements IItem.Name
        Get
            Return ItemType.Name
        End Get
    End Property

    Public ReadOnly Property ItemType As ItemTypes Implements IItem.ItemType
        Get
            Return CType(_worldData.Items(Id).ItemType, ItemTypes)
        End Get
    End Property

    Public ReadOnly Property CanEat As Boolean Implements IItem.CanEat
        Get
            Return ItemType.CanEat
        End Get
    End Property

    Public ReadOnly Property Satiety As Integer Implements IItem.Satiety
        Get
            Return ItemType.Satiety
        End Get
    End Property
End Class
