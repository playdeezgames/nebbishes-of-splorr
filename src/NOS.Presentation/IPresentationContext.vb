Public Interface IPresentationContext
    ReadOnly Property WindowTitle As String
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
    ReadOnly Property UIScale As Integer
    ReadOnly Property ScreenWidth As Integer
    ReadOnly Property ScreenHeight As Integer
    ReadOnly Property IsQuit As Boolean
    Sub HandleKey(key As Keys)
    Sub Update(ticks As Long)
    WriteOnly Property Texture As Texture2D
End Interface
