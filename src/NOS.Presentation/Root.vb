Public Class Root
    Inherits Game
    Private _graphics As GraphicsDeviceManager
    Private _spriteBatch As SpriteBatch
    Private _oldKeyboardState As New KeyboardState
    Private _keyboardState As New KeyboardState
    Private _texture As Texture2D
    Private _presentationContext As IPresentationContext
    Sub New(presentationContext As IPresentationContext)
        _presentationContext = presentationContext
        _graphics = New GraphicsDeviceManager(Me)
    End Sub
    Private Sub ResizeScreen()
        _graphics.PreferredBackBufferWidth = _presentationContext.ScreenWidth
        _graphics.PreferredBackBufferHeight = _presentationContext.ScreenHeight
        _graphics.ApplyChanges()
    End Sub
    Protected Overrides Sub Initialize()
        MyBase.Initialize()
        Window.Title = _presentationContext.WindowTitle
        ResizeScreen()
    End Sub
    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _presentationContext.ViewWidth, _presentationContext.ViewHeight)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        _oldKeyboardState = _keyboardState
        _keyboardState = Keyboard.GetState()
        For Each key In _keyboardState.GetPressedKeys()
            If Not _oldKeyboardState.IsKeyDown(key) Then
                'TODO: send to the input processor
            End If
        Next
        'TODO: update the display
        'TODO: check for the game being over
        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.Black)
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        _spriteBatch.Draw(_texture, New Rectangle(0, 0, _presentationContext.ScreenWidth, _presentationContext.ScreenHeight), Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
