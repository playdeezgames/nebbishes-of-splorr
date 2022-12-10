Public Interface IUIContext
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
    Sub Fill(x As Integer, y As Integer, width As Integer, height As Integer, hue As Hue)
    Sub SetPixel(x As Integer, y As Integer, hue As Hue)
    Sub DrawGlyph(x As Integer, y As Integer, hue As Hue, glyph As IEnumerable(Of (Integer, Integer)))
    Sub SignalExit()
    Event OnUpdate(ticks As Long)
    ReadOnly Property HasKey As Boolean
    Function ReadKey() As String
    Sub SignalUIScale(uiScale As Integer)
    Sub SetFont(fontName As String, font As Font)
    Function GetFont(fontName As String) As Font
    Sub FlushKeys()
End Interface
