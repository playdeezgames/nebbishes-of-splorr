Friend Class DropStateHandler
    Inherits BaseStateController
    Private ReadOnly _menu As Menu
    Private _menuItems As New List(Of (ItemTypes, Integer, Integer))
    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(_context, DefaultFontName, (0, 6), LineSize, (Hue.White, Hue.Black), 14, Array.Empty(Of String))
    End Sub
    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.InPlay)
            Case UpKeyName
                _menu.PreviousItem()
            Case DownKeyName
                _menu.NextItem()
            Case RightKeyName
                DropOneMore()
            Case LeftKeyName
                KeepOneMore()
            Case EnterKeyName
                DropItems()
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Private Sub DropItems()
        _world.PlayerCharacter.AttemptDropItems(_menuItems.Select(Function(x) (x.Item1, x.Item3)))
    End Sub

    Private Sub KeepOneMore()
        If _menuItems(_menu.CurrentItem).Item3 > 0 Then
            _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, _menuItems(_menu.CurrentItem).Item2 + 1, _menuItems(_menu.CurrentItem).Item3 - 1)
            UpdateMenu()
        End If
    End Sub
    Private Sub DropOneMore()
        If _menuItems(_menu.CurrentItem).Item2 > 0 Then
            _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, _menuItems(_menu.CurrentItem).Item2 - 1, _menuItems(_menu.CurrentItem).Item3 + 1)
            UpdateMenu()
        End If
    End Sub
    Protected Overrides Sub Redraw(ticks As Long)
        Dim total = _menuItems.Sum(Function(x) x.Item3)
        DefaultFont.WriteLine((0, 0), LineSize, $"Drop Items(x{total}):", Hue.Red)
        _menu.Draw()
    End Sub
    Public Overrides Sub Restart()
        MyBase.Restart()
        _menuItems = _world.PlayerCharacter.Items.GroupBy(Function(x) x.ItemType).Select(Function(x) (x.Key, x.Count, 0)).ToList
        UpdateMenu()
    End Sub
    Private Sub UpdateMenu()
        _menu.Items = _menuItems.Select(Function(x) $"{x.Item1.Name}(keep {x.Item2}, drop {x.Item3})")
    End Sub
End Class
