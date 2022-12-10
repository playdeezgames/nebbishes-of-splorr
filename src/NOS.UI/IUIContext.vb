﻿Public Interface IUIContext
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
    Sub Fill(x As Integer, y As Integer, width As Integer, height As Integer, hue As Hue)
    Sub SetPixel(x As Integer, y As Integer, hue As Hue)
    Sub SignalExit()
    Event OnUpdate()
    Event OnKey(keyName As String)
End Interface
