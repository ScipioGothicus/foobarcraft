using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MinicraftClone.Tiles;

namespace MinicraftClone;

public class Player(Texture2D texture, World world): Entity(world)
{
    private Texture2D _texture = texture;
    private World _world = world;
    private float _speed = 150.0f;
    private Vector2 _direction = Vector2.Zero;
    private Vector2 _velocity = Vector2.Zero;
    private const float MaxVelocityX = 300f;
    private const float MaxVelocityY = 1000f;
    
    public override void Update(GameTime gameTime)
    {
        Rectangle collision = Collision;
        _direction = Vector2.Zero;
        KeyboardState ks = Keyboard.GetState();

        if (ks.IsKeyDown(Keys.W))
        {
            _direction.Y = -1;
        }

        if (ks.IsKeyDown(Keys.S))
        {
            _direction.Y = 1;
        }
        
        if (ks.IsKeyDown(Keys.A))
        {
            _direction.X = -1;
        }

        if (ks.IsKeyDown(Keys.D))
        {
            _direction.X = 1;
        }
        
        if (_direction != Vector2.Zero) _direction.Normalize(); 
        
        _velocity = _direction * _speed;
        _velocity = Vector2.Clamp(_velocity, new Vector2(-MaxVelocityX, -MaxVelocityY), new Vector2(MaxVelocityX, MaxVelocityY));
        
        Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        collision.Location = Position.ToPoint();
        Collision = collision;
    }

    
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, Color.White);
    }

    public override void HandleCollision(Entity other)
    {
        
    }

    public Vector2 GetCurrentChunkPosition()
    {
        return new Vector2(float.Floor(Position.X / (Chunk.Size*Tile.Size)), float.Floor(Position.Y / (Chunk.Size*Tile.Size)));
    }
}