using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinicraftClone;

public class World(Camera camera, Texture2D texture, SpriteFont font)
{
    public Vector2 Gravity { get; private set; }
    public SpatialCollisionGrid CollisionGrid { get; private set; }
    private List<Chunk> _loadedChunks;
    private List<Entity> _entities;
    private List<Tile> _tiles;
    private Texture2D _spritesheet;
    private SpriteFont _font;
    public Player Player;
    
    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
        CollisionGrid.AddEntity(entity);
    }
    
    public void Load()
    {
        CollisionGrid = new SpatialCollisionGrid();
        Gravity = new Vector2(0.0f, 30f);
        _spritesheet = texture;
        Player = new Player(_spritesheet, this);
        _entities = new();
        _tiles = new();
        _font = font;
        
        AddEntity(Player);
    }
    
    public void Update(GameTime gameTime)
    {
        CollisionGrid.Update(gameTime);
        foreach (Entity entity in _entities) 
        {
            entity.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
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