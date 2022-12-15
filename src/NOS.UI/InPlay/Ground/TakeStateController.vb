﻿Friend Class TakeStateController
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
                TakeOneMore()
            Case LeftKeyName
                LeaveOneMore()
            Case EnterKeyName
                TakeItems()
                SetState(UIStates.InPlay)
            Case AllKeyName
                TakeAll()
            Case HalfKeyName
                TakeHalf()
            Case NoneKeyName
                TakeNone()
        End Select
    End Sub

    Private Sub TakeNone()
        Dim total = _menuItems(_menu.CurrentItem).Item2 + _menuItems(_menu.CurrentItem).Item3
        _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, total, 0)
        UpdateMenu()
    End Sub

    Private Sub TakeHalf()
        Dim total = _menuItems(_menu.CurrentItem).Item2 + _menuItems(_menu.CurrentItem).Item3
        _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, total - total \ 2, total \ 2)
        UpdateMenu()
    End Sub

    Private Sub TakeAll()
        Dim total = _menuItems(_menu.CurrentItem).Item2 + _menuItems(_menu.CurrentItem).Item3
        _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, 0, total)
        UpdateMenu()
    End Sub

    Private Sub TakeItems()
        _world.PlayerCharacter.AttemptTakeItems(_menuItems.Select(Function(x) (x.Item1, x.Item3)))
    End Sub

    Private Sub LeaveOneMore()
        If _menuItems(_menu.CurrentItem).Item3 > 0 Then
            _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, _menuItems(_menu.CurrentItem).Item2 + 1, _menuItems(_menu.CurrentItem).Item3 - 1)
            UpdateMenu()
        End If
    End Sub

    Private Sub TakeOneMore()
        If _menuItems(_menu.CurrentItem).Item2 > 0 Then
            _menuItems(_menu.CurrentItem) = (_menuItems(_menu.CurrentItem).Item1, _menuItems(_menu.CurrentItem).Item2 - 1, _menuItems(_menu.CurrentItem).Item3 + 1)
            UpdateMenu()
        End If
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim total = _menuItems.Sum(Function(x) x.Item3)
        DefaultFont.WriteLine((0, 0), LineSize, $"Take Items(x{total}):", Hue.Red)
        _menu.Draw()
    End Sub
    Public Overrides Sub Restart()
        MyBase.Restart()
        _menuItems = _world.PlayerCharacter.Location.Items.GroupBy(Function(x) x.ItemType).Select(Function(x) (x.Key, x.Count, 0)).ToList
        UpdateMenu()
    End Sub
    Private Sub UpdateMenu()
        _menu.Items = _menuItems.Select(Function(x) $"{x.Item1.Name}(leave {x.Item2}, take {x.Item3})")
    End Sub
End Class