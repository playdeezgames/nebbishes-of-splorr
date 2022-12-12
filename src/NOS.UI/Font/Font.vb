Public MustInherit Class Font
    Private ReadOnly _glyphs As IReadOnlyDictionary(Of Char, IEnumerable(Of (Integer, Integer)))
    Private ReadOnly _context As IUIContext
    Private ReadOnly _glyphWidth As Integer
    Sub New(context As IUIContext, glyphs As IReadOnlyDictionary(Of Char, IEnumerable(Of (Integer, Integer))), glyphWidth As Integer)
        _context = context
        _glyphs = glyphs
        _glyphWidth = glyphWidth
    End Sub
    Sub WriteCharacter(xy As (Integer, Integer), character As Char, hue As Hue)
        If _glyphs.ContainsKey(character) Then
            _context.DrawGlyph(xy, hue, _glyphs(character))
        End If
    End Sub
    Sub WriteString(xy As (Integer, Integer), text As String, hue As Hue)
        For Each character In text
            WriteCharacter(xy, character, hue)
            xy = (xy.Item1 + _glyphWidth, xy.Item2)
        Next
    End Sub
End Class
