Module Program
    Sub Main(args As String())
        Dim context = New NOSContext
        Dim uiController = New UIController(context)
        Using root As New Root(context)
            root.Run()
        End Using
    End Sub
End Module
