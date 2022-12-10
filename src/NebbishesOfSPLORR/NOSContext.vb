Public Class NOSContext
    Implements IPresentationContext
    Implements IUIContext

    Private Const ViewWidth = 160
    Private Const ViewHeight = 90
    Private _texture As Texture2D
    Private _buffer As Integer()
    Private _isQuit As Boolean = False
    Private _uiScale As Integer = 8
    Private _keysPressed As New Queue(Of String)

    Public ReadOnly Property WindowTitle As String Implements IPresentationContext.WindowTitle
        Get
            Return "Nebbishes of SPLORR!!"
        End Get
    End Property

    Public ReadOnly Property UIScale As Integer Implements IPresentationContext.UIScale
        Get
            Return _uiScale
        End Get
    End Property

    Public ReadOnly Property ScreenWidth As Integer Implements IPresentationContext.ScreenWidth
        Get
            Return IPresentationContext_ViewWidth * UIScale
        End Get
    End Property

    Public ReadOnly Property ScreenHeight As Integer Implements IPresentationContext.ScreenHeight
        Get
            Return IPresentationContext_ViewHeight * UIScale
        End Get
    End Property

    Public ReadOnly Property IsQuit As Boolean Implements IPresentationContext.IsQuit
        Get
            Return _isQuit
        End Get
    End Property

    Public ReadOnly Property IPresentationContext_ViewWidth As Integer Implements IPresentationContext.ViewWidth
        Get
            Return ViewWidth
        End Get
    End Property

    Public ReadOnly Property IPresentationContext_ViewHeight As Integer Implements IPresentationContext.ViewHeight
        Get
            Return ViewHeight
        End Get
    End Property

    Public WriteOnly Property Texture As Texture2D Implements IPresentationContext.Texture
        Set(value As Texture2D)
            _texture = value
            ReDim _buffer(_texture.Width * _texture.Height - 1)
        End Set
    End Property

    Private ReadOnly Property IUIContext_ViewWidth As Integer Implements IUIContext.ViewWidth
        Get
            Return ViewWidth
        End Get
    End Property

    Private ReadOnly Property IUIContext_ViewHeight As Integer Implements IUIContext.ViewHeight
        Get
            Return ViewHeight
        End Get
    End Property

    Public ReadOnly Property HasKey As Boolean Implements IUIContext.HasKey
        Get
            Return _keysPressed.Any
        End Get
    End Property

    Public Sub AddKeyPress(key As Keys) Implements IPresentationContext.AddKeyPress
        _keysPressed.Enqueue(key.ToString())
    End Sub

    Public Sub Update(ticks As Long) Implements IPresentationContext.Update
        RaiseEvent OnUpdate()
        _texture.SetData(_buffer)
    End Sub

    Public Sub Fill(x As Integer, y As Integer, width As Integer, height As Integer, hue As Hue) Implements IUIContext.Fill
        For plotY = y To y + height - 1
            For plotX = x To x + width - 1
                SetPixel(plotX, plotY, hue)
            Next
        Next
    End Sub
    Private ReadOnly hueTable As IReadOnlyDictionary(Of Hue, Integer) =
        New Dictionary(Of Hue, Integer) From
        {
            {Hue.Black, &H0},
            {Hue.Red, &HFF},
            {Hue.Blue, &HFF0000},
            {Hue.White, &HFFFFFF}
        }
    Public Event OnUpdate As IUIContext.OnUpdateEventHandler Implements IUIContext.OnUpdate
    Public Event OnUIScale() Implements IPresentationContext.OnUIScale

    Public Sub SetPixel(plotX As Integer, plotY As Integer, hue As Hue) Implements IUIContext.SetPixel
        If plotY >= 0 AndAlso plotX >= 0 AndAlso plotY < _texture.Height AndAlso plotX < _texture.Width Then
            _buffer(plotY * _texture.Width + plotX) = hueTable(hue)
        End If
    End Sub

    Public Sub SignalExit() Implements IUIContext.SignalExit
        _isQuit = True
    End Sub

    Public Sub SignalUIScale(uiScale As Integer) Implements IUIContext.SignalUIScale
        _uiScale = uiScale
        RaiseEvent OnUIScale()
    End Sub

    Public Sub DrawGlyph(x As Integer, y As Integer, hue As Hue, glyph As IEnumerable(Of (Integer, Integer))) Implements IUIContext.DrawGlyph
        For Each pixel In glyph
            SetPixel(x + pixel.Item1, y + pixel.Item2, hue)
        Next
    End Sub
    Private ReadOnly _fonts As New Dictionary(Of String, Font)
    Public Function GetFont(fontName As String) As Font Implements IUIContext.GetFont
        Return _fonts(fontName)
    End Function

    Public Sub SetFont(fontName As String, font As Font) Implements IUIContext.SetFont
        _fonts(fontName) = font
    End Sub

    Public Function ReadKey() As String Implements IUIContext.ReadKey
        Return _keysPressed.Dequeue
    End Function
End Class
