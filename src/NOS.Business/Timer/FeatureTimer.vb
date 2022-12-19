Public Class FeatureTimer
    Inherits BaseThingie
    Implements ITimer
    Private ReadOnly _featureId As Integer

    Friend Sub New(worldData As WorldData, world As IWorld, featureId As Integer, timerType As TimerTypes)
        MyBase.New(worldData, world)
        Me.TimerType = timerType
        _featureId = featureId
    End Sub

    Public ReadOnly Property TimerType As TimerTypes Implements ITimer.TimerType

    Public Sub Destroy() Implements ITimer.Destroy
        _worldData.Features(_featureId).Timers.Remove(TimerType)
    End Sub

    Public Function Advance() As Boolean Implements ITimer.Advance
        Dim timerValue = _worldData.Features(_featureId).Timers(TimerType)
        timerValue(0) += 1
        Dim result As Boolean = timerValue(0) >= timerValue(1)
        If timerValue(0) >= timerValue(1) Then
            timerValue(0) -= timerValue(1)
        End If
        Return result
    End Function
End Class
