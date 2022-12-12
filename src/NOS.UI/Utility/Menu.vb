Public Class Menu
    Private ReadOnly _items As String()
    Private _item As Integer
    Private ReadOnly _x As Integer
    Private ReadOnly _y As Integer
    Private ReadOnly _width As Integer
    Private ReadOnly _lineHeight As Integer
    Private ReadOnly _foreground As Hue
    Private ReadOnly _background As Hue
    Private ReadOnly _uiContext As IUIContext
    Private ReadOnly _fontName As String
    Sub New(uiContext As IUIContext, fontName As String, xy As (Integer, Integer), itemSize As (Integer, Integer), colors As (Hue, Hue), ParamArray items As String())
        _uiContext = uiContext
        _fontName = fontName
        _x = xy.Item1
        _y = xy.Item2
        _width = itemSize.Item1
        _lineHeight = itemSize.Item2
        _item = 0
        _items = items
        _foreground = colors.Item1
        _background = colors.Item2
    End Sub
    Friend Sub Draw()
        Dim font = _uiContext.GetFont(_fontName)
        For index = 0 To _items.Length - 1
            Dim y = _y + _lineHeight * index
            If index = _item Then
                _uiContext.Fill(_x, y, _width, _lineHeight, _foreground)
                font.WriteString((_x, y), _items(index), _background)
            Else
                _uiContext.Fill(_x, y, _width, _lineHeight, _background)
                font.WriteString((_x, y), _items(index), _foreground)
            End If
        Next
    End Sub

    Friend Sub PreviousItem()
        _item = (_item + _items.Length - 1) Mod _items.Length
    End Sub

    Friend Sub NextItem()
        _item = (_item + 1) Mod _items.Length
    End Sub

    Friend ReadOnly Property CurrentItem As Integer
        Get
            Return _item
        End Get
    End Property
End Class
