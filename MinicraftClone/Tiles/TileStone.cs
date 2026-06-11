using Microsoft.Xna.Framework;

namespace MinicraftClone.Tiles;

public class TileStone : Tile
{
    public TileStone(Chunk chunk, Vector2 position) : base(chunk, position)
    {
        TileColor = new Color(120, 120, 120, 255);
        TextureRegion = new Rectangle(1*Size, 1*Size, Size, Size);
    }
}