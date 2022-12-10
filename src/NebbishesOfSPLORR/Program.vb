Module Program
    Sub Main(args As String())
        Using root As New Root(New NOSContext)
            root.Run()
        End Using
    End Sub
End Module
