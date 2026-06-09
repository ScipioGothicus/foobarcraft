using Microsoft.Xna.Framework;

namespace MinicraftClone;

public class Tile(Chunk chunk, Vector2 position)
{
    public bool Collidable { get; private set; }
    public Color TileColor { get; private set; }
    public Vector2 Position { get; private set; } = position;
    public Chunk ParentChunk { get; private set; } = chunk;

    public const int Size = 16;

}