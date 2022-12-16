Friend Class ChooseFeatureStateController
    Inherits BaseStateController
    Private _menu As Menu
    Private _features As List(Of IFeature)
    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
        _menu = New Menu(_context, DefaultFontName, (0, 6), LineSize, (Hue.White, Hue.Black), 14)
    End Sub
    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.InPlay)
            Case NorthKeyName
                _menu.PreviousItem()
            Case SouthKeyName
                _menu.NextItem()
            Case EnterKeyName, SpaceKeyName
                _world.InteractionFeature = _features(_menu.CurrentItem)
                SetState(UIStates.InPlay)
        End Select
    End Sub
    Protected Overrides Sub Redraw(ticks As Long)
        DefaultFont.Write((0, 0), "Interact With...", Hue.Blue)
        _menu.Draw()
    End Sub
    Public Overrides Sub Restart()
        MyBase.Restart()
        _features = _world.PlayerCharacter.Location.Features.ToList
        _menu.Items = _features.Select(Function(x) x.Name)
    End Sub
End Class
