using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinicraftClone.Tiles;

public class TileGrass : Tile
{
    public TileGrass(Chunk chunk, Vector2 position) : base(chunk, position)
    {
        TileColor = new Color(121, 223, 65, 255);
        TextureRegion = new Rectangle(1*Size, 1*Size, Size, Size);
    }
}