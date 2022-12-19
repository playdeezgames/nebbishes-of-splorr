Public Interface IItemHolder
    Sub AddItem(item As IItem)
    Sub RemoveItem(item As IItem)
    ReadOnly Property Items As IEnumerable(Of IItem)
    ReadOnly Property HasItems As Boolean
End Interface
