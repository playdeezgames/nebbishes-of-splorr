Public Class Root
    Inherits Game
    Private _uiScale As Integer
    Private _graphics As GraphicsDeviceManager
    Private _spriteBatch As SpriteBatch
    Private _oldKeyboardState As New KeyboardState
    Private _keyboardState As New KeyboardState
    Private _texture As Texture2D
    Private ReadOnly _viewWidth As Integer
    Private ReadOnly _viewHeight As Integer
    Private ReadOnly _windowTitle As String
    Private _presentationContext As IPresentationContext
    Sub New(presentationContext As IPresentationContext, windowTitle As String, viewWidth As Integer, viewHeight As Integer, uiScale As Integer)
        _presentationContext = presentationContext
        _uiScale = uiScale
        _viewHeight = viewHeight
        _viewWidth = viewWidth
        _windowTitle = windowTitle
        _graphics = New GraphicsDeviceManager(Me)
    End Sub
    Private Sub ResizeScreen()
        _graphics.PreferredBackBufferWidth = _viewWidth * _uiScale
        _graphics.PreferredBackBufferHeight = _viewHeight * _uiScale
        _graphics.ApplyChanges()
    End Sub
    Private Sub SetUIScale(newScale As Integer)
        _uiScale = newScale
        ResizeScreen()
    End Sub
    Protected Overrides Sub Initialize()
        MyBase.Initialize()
        Window.Title = _windowTitle
        ResizeScreen()
    End Sub
    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _viewWidth, _viewHeight)
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
        _spriteBatch.Draw(_texture, New Rectangle(0, 0, _viewWidth * _uiScale, _viewHeight * _uiScale), Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
