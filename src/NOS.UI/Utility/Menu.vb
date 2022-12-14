Public Class Menu
    Private _items As String()
    Private _item As Integer
    Private ReadOnly _x As Integer
    Private ReadOnly _y As Integer
    Private ReadOnly _width As Integer
    Private ReadOnly _lineHeight As Integer
    Private ReadOnly _foreground As Hue
    Private ReadOnly _background As Hue
    Private ReadOnly _uiContext As IUIContext
    Private ReadOnly _fontName As String
    Private ReadOnly _lineCount As Integer
    Private _lineIndex As Integer
    Sub New(uiContext As IUIContext, fontName As String, xy As (Integer, Integer), itemSize As (Integer, Integer), colors As (Hue, Hue), lineCount As Integer, ParamArray items As String())
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
        _lineCount = lineCount
        _lineIndex = 0
    End Sub
    Friend Sub Draw()
        If Not _items.Any Then
            Return
        End If
        Dim font = _uiContext.GetFont(_fontName)
        For line = 0 To _lineCount - 1
            Dim index = line + _lineIndex
            Dim y = _y + _lineHeight * line
            If index >= _items.Length Then
                'nothing!
            ElseIf index = _item Then
                _uiContext.Fill(_x, y, _width, _lineHeight, _foreground)
                font.Write((_x, y), _items(index), _background)
            Else
                _uiContext.Fill(_x, y, _width, _lineHeight, _background)
                font.Write((_x, y), _items(index), _foreground)
            End If
        Next
    End Sub

    Friend Sub PreviousItem()
        If Not _items.Any Then
            Return
        End If
        _item = (_item + _items.Length - 1) Mod _items.Length
        AdjustLineIndex()
    End Sub

    Friend Sub NextItem()
        If Not _items.Any Then
            Return
        End If
        _item = (_item + 1) Mod _items.Length
        AdjustLineIndex()
    End Sub

    Private Sub AdjustLineIndex()
        If _item < _lineIndex Then
            _lineIndex = _item
            Return
        End If
        If _item >= _lineIndex + _lineCount Then
            _lineIndex = _item - (_lineCount - 1)
        End If
    End Sub
    Friend ReadOnly Property CurrentItem As Integer
        Get
            Return _item
        End Get
    End Property
    Friend Property Items As IEnumerable(Of String)
        Get
            Return _items
        End Get
        Set(value As IEnumerable(Of String))
            _items = value.ToArray
        End Set
    End Property
End Class
