using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinicraftClone;

public class DebugEntity : Entity
{
    private Vector2 _lastPosition;
    private SpriteFont _font;
    private Random _rand;
    private Vector2 _direction;
    private Color _color;
    private Texture2D _texture;
    private float _speed;
    
    public DebugEntity(Texture2D texture, World world, SpriteFont font) : base(world)
    {
        _font = font;
        _rand = new Random();
        Position = new Vector2(_rand.Next(0, 400), _rand.Next(0, 400));
        Collision = new Rectangle((int)Position.X, (int)Position.Y, 8, 8);
        _direction = new Vector2(1,1);
        _color = Color.Green;
        _texture = texture;
        _speed = 50f;
    }

    public override void Move(GameTime gameTime)
    {
        _lastPosition = Position;
        Rectangle collision = Collision;
        Grid.Move(this);
        
        if (Position.X > 600 || Position.X < 0)
        {
            _direction.X = -_direction.X;
        }

        if (Position.Y > 400 || Position.Y < 0)
        {
            _direction.Y = -_direction.Y;
        }
        
        Position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        collision.Location = Position.ToPoint();
        Collision = collision;
    }
    
    public override void Update(GameTime gameTime)
    {
        Move(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, Collision, _color);
    }

    public override void HandleCollision(Entity other)
    {
        Rectangle collision = Collision;
        Position = _lastPosition;
        collision.Location = _lastPosition.ToPoint();
        Collision = collision;
        
        if (Position.X > other.Position.X || Position.X < other.Position.X)
        {
            _direction.X = -_direction.X;
        }

        if (Position.Y > other.Position.Y || Position.Y < other.Position.Y)
        {
            _direction.Y = -_direction.Y;
        }
    }
}