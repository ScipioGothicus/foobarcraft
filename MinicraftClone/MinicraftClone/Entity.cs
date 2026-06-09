using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinicraftClone;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Vector2 CurrentCell { get; set; }
    public List<Vector2> SurroundingCells { get; set; }
    public Rectangle Collision { get; set; }
    public SpatialCollisionGrid Grid { get; set; }
    public World World { get; set; }

    protected Entity(World world)
    {
        World = world;
        Grid = world.CollisionGrid;
    }

    public virtual void Move(GameTime gameTime)
    {
        Grid.Move(this);
    }
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract void HandleCollision(Entity other);
}