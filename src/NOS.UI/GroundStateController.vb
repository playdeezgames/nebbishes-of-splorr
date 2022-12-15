Friend Class GroundStateController
    Inherits BaseStateController

    Public Sub New(context As IUIContext, world As IWorld)
        MyBase.New(context, world)
    End Sub

    Protected Overrides Sub HandleKey(keyName As String)
        Select Case keyName
            Case EscapeKeyName
                SetState(UIStates.InPlay)
        End Select
    End Sub

    Protected Overrides Sub Redraw(ticks As Long)
        Dim y = DefaultFont.WriteLine((0, 0), LineSize, "On the ground:", Hue.White)
        Dim location = _world.PlayerCharacter.Location
        If Not location.HasItems Then
            y = DefaultFont.WriteLine((0, y), LineSize, "(nothing)", Hue.Blue)
            Return
        End If
        Dim itemGroups = location.Items.GroupBy(Function(x) x.ItemType)
        For Each itemGroup In itemGroups
            y = DefaultFont.WriteLine((0, y), LineSize, $"{itemGroup.Key.Name}(x{itemGroup.Count})", Hue.Blue)
        Next
    End Sub
End Class
