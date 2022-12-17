Public Class CharacterTimer
    Inherits BaseThingie
    Implements ITimer
    Private ReadOnly _characterId As Integer
    Private ReadOnly _timerType As TimerTypes

    Friend Sub New(worldData As WorldData, world As IWorld, characterId As Integer, timerType As TimerTypes)
        MyBase.New(worldData, world)
        _characterId = characterId
        _timerType = timerType
    End Sub

    Public Function Advance() As Boolean Implements ITimer.Advance
        Dim timerValue = _worldData.Characters(_characterId).Timers(_timerType)
        timerValue(0) += 1
        Dim result As Boolean = False
        If timerValue(0) >= timerValue(1) Then
            timerValue(0) -= timerValue(1)
        End If
        Return result
    End Function
End Class
