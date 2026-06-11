using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinicraftClone.Tiles;

namespace MinicraftClone;

public class Chunk(Vector2 offset)
{
    public static int Size = 16;
    public Tile[,] Tiles = new Tile[Size,Size];
    public Vector2 Offset = offset;
    public Vector2 Position = offset * Size * Tile.Size;
    
    public void Update(GameTime gameTime)
    {
        foreach (Tile tile in Tiles)
        {
            tile.Update(gameTime);
        }
    }

    public void Draw(Texture2D spritesheet, SpriteBatch spriteBatch)
    {
        foreach (Tile tile in Tiles)
        {
            tile.Draw(spritesheet, spriteBatch);
        }
    }
}