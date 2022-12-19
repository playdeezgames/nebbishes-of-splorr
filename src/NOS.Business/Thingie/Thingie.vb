Public MustInherit Class Thingie
    Inherits BaseThingie
    Implements IThingie
    Public ReadOnly Property Id As Integer Implements IThingie.Id
    Friend Sub New(worldData As WorldData, world As IWorld, id As Integer)
        MyBase.New(worldData, world)
        Me.Id = id
    End Sub
    Public Overridable Sub NextRound() Implements IThingie.NextRound
        AdvanceTimers()
    End Sub
    Protected MustOverride ReadOnly Property Timers As IEnumerable(Of ITimer)
    Private Sub AdvanceTimers()
        For Each timer In Timers
            If timer.Advance() Then
                TriggerTimer(timer)
            End If
        Next
    End Sub
    Private Sub TriggerTimer(timer As ITimer)
        timer.Destroy()
        OnTriggerTimer(timer.TimerType)
    End Sub
    Protected MustOverride Sub OnTriggerTimer(timerType As TimerTypes)
End Class
