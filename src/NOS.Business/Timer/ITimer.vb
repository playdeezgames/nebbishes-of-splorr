Public Interface ITimer
    Function Advance() As Boolean
    Sub Destroy()
    ReadOnly Property TimerType As TimerTypes
End Interface
