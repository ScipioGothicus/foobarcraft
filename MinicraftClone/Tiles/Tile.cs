using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinicraftClone.Tiles;

public abstract class Tile(Chunk chunk, Vector2 position)
{
    public bool Collidable { get; private set; }
    public float Hardness { get; private set; }
    public Color TileColor { get; init; }
    public Vector2 Position { get; private set; } = position;
    public Chunk ParentChunk { get; private set; } = chunk;
    
    public Rectangle TextureRegion { get; init; }

    public static int Size = 8;

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw(Texture2D spritesheet, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spritesheet, Position, TextureRegion, TileColor);
    }
    
}