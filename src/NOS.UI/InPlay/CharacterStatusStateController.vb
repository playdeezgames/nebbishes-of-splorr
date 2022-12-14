Friend Class CharacterStatusStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.InPlay)
            Case HelpKeyName
                SetState(UIStates.GeneralHelp)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim font = _context.GetFont(DefaultFontName)
        Dim character = _world.PlayerCharacter
        Dim y = font.WriteLine((0, 0), (160, 6), $"{character.Name} Status:", Hue.White)
        y = font.WriteLine((0, y), (160, 6), $"Energy: {character.Energy}/{character.MaximumEnergy}", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"Satiety: {character.Satiety}/{character.MaximumSatiety}", Hue.Blue)
        y = font.WriteLine((0, y), (160, 6), $"Health: {character.Health}/{character.MaximumHealth}", Hue.Blue)
    End Sub
End Class
