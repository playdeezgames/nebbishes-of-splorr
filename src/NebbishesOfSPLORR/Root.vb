Public Class Root
    Inherits Game
    Private uiScale As Integer = 4
    Private graphics As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch
    Private oldKeyboardState As New KeyboardState
    Private keyboardState As New KeyboardState
    Private Const ScreenWidth = 160
    Private Const ScreenHeight = 90
    Sub New()
        graphics = New GraphicsDeviceManager(Me)
    End Sub
    Private Sub ResizeScreen()
        graphics.PreferredBackBufferWidth = ScreenWidth * uiScale
        graphics.PreferredBackBufferHeight = ScreenHeight * uiScale
        graphics.ApplyChanges()
    End Sub
    Private Sub SetUIScale(newScale As Integer)
        uiScale = newScale
        ResizeScreen()
    End Sub
    Protected Overrides Sub Initialize()
        MyBase.Initialize()
        Window.Title = "Nebbishes of SPLORR!!"
        Content.RootDirectory = "Content"
        ResizeScreen()
    End Sub
    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        spriteBatch = New SpriteBatch(GraphicsDevice)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        oldKeyboardState = keyboardState
        keyboardState = Keyboard.GetState()
        For Each key In keyboardState.GetPressedKeys()
            If Not oldKeyboardState.IsKeyDown(key) Then
                'TODO: send to the input processor
            End If
        Next
        'TODO: update the display
        'TODO: check for the game being over
        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.Black)
        spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        'TODO: draw things
        spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
