Public Interface ITimer
    Function Advance() As Boolean
    ReadOnly Property TimerType As TimerTypes
End Interface
