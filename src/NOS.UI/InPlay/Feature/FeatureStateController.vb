Friend Class FeatureStateController
    Inherits BaseStateController
    Private _menu As Menu
    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(_context, DefaultFontName, (0, 6), LineSize, (Hue.White, Hue.Black), 14)
    End Sub
    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                _world.InteractionFeature = Nothing
                SetState(UIStates.InPlay)
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EnterKeyName, SpaceKeyName
                Select Case _menu.CurrentItem
                    Case 0
                        If _world.InteractionFeature.Items.Any Then
                            SetState(UIStates.RemoveFeatureItems)
                        End If
                End Select
        End Select
    End Sub
    Protected Overrides Sub Redraw(ticks As Long)
        If _world.InteractionFeature Is Nothing Then
            Return
        End If
        DefaultFont.Write((0, 0), _world.InteractionFeature.Name, Hue.Blue)
        _menu.Draw()
    End Sub
    Public Overrides Sub Restart()
        MyBase.Restart()
        UpdateMenu()
    End Sub
    Private Sub UpdateMenu()
        _menu.Items = New List(Of String) From {$"Remove Items(x{_world.InteractionFeature.Items.Count})"}
    End Sub
End Class
