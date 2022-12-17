Public Class CharacterTimer
    Inherits BaseThingie
    Implements ITimer
    Private ReadOnly _characterId As Integer
    Public ReadOnly Property TimerType As TimerTypes Implements ITimer.TimerType

    Friend Sub New(worldData As WorldData, world As IWorld, characterId As Integer, timerType As TimerTypes)
        MyBase.New(worldData, world)
        _characterId = characterId
        Me.TimerType = timerType
    End Sub


    Public Function Advance() As Boolean Implements ITimer.Advance
        Dim timerValue = _worldData.Characters(_characterId).Timers(TimerType)
        timerValue(0) += 1
        Dim result As Boolean = timerValue(0) >= timerValue(1)
        If timerValue(0) >= timerValue(1) Then
            timerValue(0) -= timerValue(1)
        End If
        Return result
    End Function
End Class
