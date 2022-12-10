Module Program
    Sub Main(args As String())
        Using root As New Root(New PresentationContext, "Nebbishes of SPLORR!!", 160, 90, 8)
            root.Run()
        End Using
    End Sub
End Module
