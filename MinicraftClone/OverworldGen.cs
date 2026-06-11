using Microsoft.Xna.Framework;
using MinicraftClone.Tiles;

namespace MinicraftClone;

public class OverworldGen(long seed)
{
    public long Seed { get; private set; } = seed;
    public float ScaleFactor { get; private set; } = 1000.0f;

    public Chunk GenerateChunk(Vector2 position)
    {
        Chunk chunk = new Chunk(position);
        for (int x = 0; x < Chunk.Size; x++)
        {
            for (int y = 0; y < Chunk.Size; y++)
            {
                float noise = OpenSimplex2S.Noise2(Seed, (chunk.Position.X+x*Tile.Size)/ScaleFactor, (chunk.Position.Y+y*Tile.Size)/ScaleFactor);
                if (noise <= 0.3f)
                {
                    chunk.Tiles[x, y] = new TileGrass(chunk, new Vector2(chunk.Position.X+x*Tile.Size, chunk.Position.Y+y*Tile.Size));
                }
                else
                {
                    chunk.Tiles[x, y] = new TileStone(chunk, new Vector2(chunk.Position.X+x*Tile.Size, chunk.Position.Y+y*Tile.Size));
                }
            }
        }

        return chunk;
    }

}