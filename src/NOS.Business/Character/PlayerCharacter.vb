Friend Class PlayerCharacter
    Inherits Character

    Public Sub New(worldData As WorldData, world As World, id As Integer)
        MyBase.New(worldData, world, id)
    End Sub
    Public Overrides Function AttemptForage() As Boolean
        If MyBase.AttemptForage() Then
            World.NextRound()
            Return True
        End If
        Return False
    End Function
    Public Overrides Function AttemptMove(direction As Directions) As Boolean
        If MyBase.AttemptMove(direction) Then
            World.NextRound()
            Return True
        End If
        Return False
    End Function
    Public Overrides Function AttemptSleep() As Boolean
        If MyBase.AttemptSleep() Then
            Dim oldEnergy = Energy
            Dim minutes = 0
            While IsSleeping
                World.NextRound()
                minutes += 1
            End While
            AddMessage($"After {minutes} minutes asleep, {Name} gains {Energy - oldEnergy} energy.")
            Return True
        End If
        Return False
    End Function
End Class
