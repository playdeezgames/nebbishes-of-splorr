Friend Class CharacterStatusStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        SetState(UIStates.InPlay)
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim character = _world.PlayerCharacter
        Dim y = font.WriteLine((0, 0), (160, 6), $"{character.Name} Status:", Hue.White)
        y = font.WriteLine((0, y), (160, 6), $"Energy: {character.Energy}/{character.MaximumEnergy}", Hue.Blue)
    End Sub
End Class
