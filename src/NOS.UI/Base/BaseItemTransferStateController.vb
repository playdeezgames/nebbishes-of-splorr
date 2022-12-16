Friend MustInherit Class BaseItemTransferStateController
    Inherits BaseStateController
    Private ReadOnly _menu As Menu
    Private _menuItems As New List(Of (ItemTypes, Integer, Integer))
    Private ReadOnly _cancelState As UIStates
    Private ReadOnly _postCommitState As UIStates
    Private ReadOnly _headerHue As Hue
    Public Sub New(context As IUIContext, world As IWorld, cancelState As UIStates, postCommitState As UIStates, headerHue As Hue)
        MyBase.New(context, world)
        _menu = New Menu(_context, DefaultFontName, (0, 6), LineSize, (Hue.White, Hue.Black), 14, Array.Empty(Of String))
        _cancelState = cancelState
        _postCommitState = postCommitState
        _headerHue = headerHue
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(_cancelState)
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EastKeyName
                TransferOneMore()
            Case WestKeyName
                LeaveOneMore()
            Case EnterKeyName
                DoTransfer()
                SetState(_postCommitState)
            Case AllKeyName
                TransferAll()
            Case HalfKeyName
                TransferHalf()
            Case NoneKeyName
                TransferNone()
        End Select
    End Sub
    Private Sub TransferNone()
        Dim total = _menuItems(_menu.CurrentItem).Item2 + _menuItems(_menu.CurrentItem).Item3
        _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, total, 0)
        UpdateMenu()
    End Sub

    Private Sub TransferHalf()
        Dim total = _menuItems(_menu.CurrentItem).Item2 + _menuItems(_menu.CurrentItem).Item3
        _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, total - total \ 2, total \ 2)
        UpdateMenu()
    End Sub

    Private Sub TransferAll()
        Dim total = _menuItems(_menu.CurrentItem).Item2 + _menuItems(_menu.CurrentItem).Item3
        _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, 0, total)
        UpdateMenu()
    End Sub

    Protected MustOverride Sub DoTransfer()

    Private Sub LeaveOneMore()
        If _menuItems(_menu.CurrentItem).Item3 > 0 Then
            _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, _menuItems(_menu.CurrentItem).Item2 + 1, _menuItems(_menu.CurrentItem).Item3 - 1)
            UpdateMenu()
        End If
    End Sub

    Private Sub TransferOneMore()
        If _menuItems(_menu.CurrentItem).Item2 > 0 Then
            _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, _menuItems(_menu.CurrentItem).Item2 - 1, _menuItems(_menu.CurrentItem).Item3 + 1)
            UpdateMenu()
        End If
    End Sub
    Protected MustOverride ReadOnly Property Header As String
    Protected Overrides Sub Redraw(ticks As Long)
        Dim total = _menuItems.Sum(Function(x) x.Item3)
        DefaultFont.WriteLine((0, 0), LineSize, Header, _headerHue)
        _menu.Draw()
    End Sub
    Protected MustOverride ReadOnly Property ItemSource As IEnumerable(Of IItem)
    Public Overrides Sub Restart()
        MyBase.Restart()
        _menuItems = ItemSource.GroupBy(Function(x) x.ItemType).Select(Function(x) (x.Key, x.Count, 0)).ToList
        UpdateMenu()
    End Sub
    Protected MustOverride Function MenuItemText(itemName As String, availableQuantity As Integer, transferQuantity As Integer) As String
    Private Sub UpdateMenu()
        _menu.Items = _menuItems.Select(Function(x) MenuItemText(x.Item1.Name, x.Item2, x.Item3))
    End Sub
End Class
