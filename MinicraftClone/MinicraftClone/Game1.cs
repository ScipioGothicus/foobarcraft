using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MinicraftClone;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private long _seed;
    private Texture2D _debugNoiseTexture;
    private Camera _camera;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private Texture2D _texture;
    private World _world;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _camera = new Camera(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, new Vector2(0,0));
        _seed = 615152118;
       base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _texture = Content.Load<Texture2D>("player-placeholder");
        _font = Content.Load<SpriteFont>("FoobitsFont");
        _world = new World(_camera, _texture, _font);
        _world.Load();
        
        float scaleFactor = 1000.0f; // scale of 1000 would be best for small biomes from what I can tell.
        _debugNoiseTexture = _world.CreateTexture(_graphics.GraphicsDevice, 256, 256, pixel => 
            new Color(
                OpenSimplex2S.Noise2(_seed, pixel.Item1/scaleFactor, pixel.Item2/scaleFactor), 
                OpenSimplex2S.Noise2(_seed, pixel.Item1/scaleFactor, pixel.Item2/scaleFactor), 
                OpenSimplex2S.Noise2(_seed, pixel.Item1/scaleFactor, pixel.Item2/scaleFactor)
                , 1.0f)
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _camera.Update(_world.Player.Position, gameTime); // shit sucks don't get the world's player's position please :)

        _world.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.TransformMatrix);
        _spriteBatch.Draw(_debugNoiseTexture, Vector2.Zero, Color.White);
        _world.Draw(_spriteBatch);
        _spriteBatch.End();
        
        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, _world.Player.Position.ToString(), new Vector2(10, 10), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}