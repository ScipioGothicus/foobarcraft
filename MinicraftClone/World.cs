using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinicraftClone.Tiles;

namespace MinicraftClone;

public class World(Camera camera, Texture2D texture, SpriteFont font)
{
    public SpatialCollisionGrid CollisionGrid { get; private set; }
    private OverworldGen _overworldGen;
    private Dictionary<Vector2, Chunk> _loadedChunks;
    private List<Entity> _entities;
    private Texture2D _spritesheet;
    private SpriteFont _font;
    public Player Player;
    public const int RenderDistance = 16;
    
    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
        CollisionGrid.AddEntity(entity);
    }
    
    public void Load()
    {
        CollisionGrid = new SpatialCollisionGrid();
        _spritesheet = texture;
        Player = new Player(_spritesheet, this);
        _entities = new();
        _font = font;
        _overworldGen = new OverworldGen(615152118);

        _loadedChunks = new Dictionary<Vector2, Chunk>();
        
        AddEntity(Player);
    }
    
    public void Update(GameTime gameTime)
    {
        CollisionGrid.Update(gameTime);

        Vector2 playerChunkPosition = Player.GetCurrentChunkPosition();
            
        for (int x = (int)playerChunkPosition.X - RenderDistance/2; x < (int)playerChunkPosition.X + RenderDistance/2; x++)
        {
            for (int y = (int)playerChunkPosition.Y - RenderDistance/2; y < (int)playerChunkPosition.Y + RenderDistance/2; y++)
            {
                if (!_loadedChunks.ContainsKey(new Vector2(x, y)))
                {
                    _loadedChunks.Add(new Vector2(x, y), _overworldGen.GenerateChunk(new Vector2(x, y)));
                }
            }
        }
        
        foreach (Chunk chunk in _loadedChunks.Values)
        {
            chunk.Update(gameTime);
        }
        
        foreach (Entity entity in _entities) 
        {
            entity.Update(gameTime);
        }

        // unload chunks outside the render distance
        _loadedChunks = _loadedChunks.Where(chunk => 
            chunk.Key.X < RenderDistance + Player.GetCurrentChunkPosition().X &&
            chunk.Key.X > -RenderDistance + Player.GetCurrentChunkPosition().X && 
            chunk.Key.Y < RenderDistance + Player.GetCurrentChunkPosition().Y && 
            chunk.Key.Y > -RenderDistance + Player.GetCurrentChunkPosition().Y
        ).ToDictionary();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Chunk chunk in _loadedChunks.Values)
        {
            chunk.Draw(_spritesheet, spriteBatch);
        }
        
        foreach (Entity entity in _entities)
        {
            entity.Draw(spriteBatch);
        }
    }

// Source - https://stackoverflow.com/a/32429364
// Posted by GMich
// Retrieved 2026-06-06, License - CC BY-SA 3.0
// slightly modified to create a texture in a 2d array that is converted to a 1d array

    public Texture2D CreateTexture(GraphicsDevice device, int width,int height, Func<(int,int),Color> paint)
    {
        //initialize a texture
        Texture2D texture = new Texture2D(device, width, height);

        //the array holds the color for each pixel in the texture
        Color[,] data = new Color[width, height];
        Color[] pixels = new Color[width * height];
        
        for(int x=0;x<data.GetLength(0);x++)
        {
            for (int y = 0; y < data.GetLength(1); y++)
            {
                data[x, y] = paint((x,y));    
            }
        }

        pixels = data.Cast<Color>().ToArray();
        //set the color
        texture.SetData(pixels);

        return texture;
    }

}