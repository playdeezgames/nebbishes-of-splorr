Public Class NOSContext
    Implements IPresentationContext

    Public ReadOnly Property WindowTitle As String Implements IPresentationContext.WindowTitle
        Get
            Return "Nebbishes of SPLORR!!"
        End Get
    End Property

    Public ReadOnly Property ViewWidth As Integer Implements IPresentationContext.ViewWidth
        Get
            Return 160
        End Get
    End Property

    Public ReadOnly Property ViewHeight As Integer Implements IPresentationContext.ViewHeight
        Get
            Return 90
        End Get
    End Property

    Public ReadOnly Property UIScale As Integer Implements IPresentationContext.UIScale
        Get
            Return 4
        End Get
    End Property

    Public ReadOnly Property ScreenWidth As Integer Implements IPresentationContext.ScreenWidth
        Get
            Return ViewWidth * UIScale
        End Get
    End Property

    Public ReadOnly Property ScreenHeight As Integer Implements IPresentationContext.ScreenHeight
        Get
            Return ViewHeight * UIScale
        End Get
    End Property
End Class
