Public Class Item
    Implements IItem
    Private ReadOnly _worldData As WorldData
    Public ReadOnly Property Id As Integer Implements IItem.Id
    Sub New(worldData As WorldData, id As Integer)
        _worldData = worldData
        Me.Id = id
    End Sub

    Friend Shared Function Create(worldData As WorldData, itemType As ItemTypes) As IItem
        Dim id = If(worldData.Items.Any, worldData.Items.Keys.Max + 1, 0)
        worldData.Items.Add(id, New ItemData With
                                {
                                    .ItemType = itemType
                                })
        Return New Item(worldData, id)
    End Function

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
End Class
