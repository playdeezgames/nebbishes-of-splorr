Public MustInherit Class Font
    Private ReadOnly _glyphs As IReadOnlyDictionary(Of Char, IEnumerable(Of (Integer, Integer)))
    Private ReadOnly _context As IUIContext
    Private ReadOnly _glyphWidth As Integer
    Sub New(context As IUIContext, glyphs As IReadOnlyDictionary(Of Char, IEnumerable(Of (Integer, Integer))), glyphWidth As Integer)
        _context = context
        _glyphs = glyphs
        _glyphWidth = glyphWidth
    End Sub
    Sub Write(xy As (Integer, Integer), character As Char, hue As Hue)
        If _glyphs.ContainsKey(character) Then
            _context.DrawGlyph(xy, hue, _glyphs(character))
        End If
    End Sub
    Sub Write(xy As (Integer, Integer), text As String, hue As Hue)
        Write(xy, (Integer.MaxValue - xy.Item1, 0), text, hue)
    End Sub
    Function Write(xy As (Integer, Integer), wh As (Integer, Integer), text As String, hue As Hue) As Integer
        Dim x As Integer = xy.Item1
        Dim y As Integer = xy.Item2
        For Each character In text
            If x >= xy.Item1 + wh.Item1 Then
                x = xy.Item1
                y = y + wh.Item2
            End If
            Write((x, y), character, hue)
            x += _glyphWidth
        Next
        Return y
    End Function
    Function WriteLine(xy As (Integer, Integer), wh As (Integer, Integer), text As String, hue As Hue) As Integer
        Return Write(xy, wh, text, hue) + wh.Item2
    End Function
End Class
