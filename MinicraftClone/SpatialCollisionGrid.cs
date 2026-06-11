using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MinicraftClone;

public class SpatialCollisionGrid()
{
    private Dictionary<Vector2, LinkedList<Entity>> _cells = new();
    private const int CellSize = 32;

    public void AddEntity(Entity entity)
    {
        Vector2 cellPosition = entity.Position / CellSize;
        cellPosition = Vector2.Floor(cellPosition);
        
        if (!_cells.TryGetValue(cellPosition, out LinkedList<Entity> value))
        {
            value = new LinkedList<Entity>();
            _cells.Add(cellPosition, value);
        }
        
        value.AddLast(entity);
        entity.CurrentCell = cellPosition;
        entity.SurroundingCells = new List<Vector2>
        {
            new(cellPosition.X-1, cellPosition.Y-1),
            new(cellPosition.X-1, cellPosition.Y),
            new(cellPosition.X-1, cellPosition.Y+1),
            new(cellPosition.X, cellPosition.Y+1),
        };
    }

    // this code is fucked up bruh :(
    public void Update(GameTime gameTime)
    {
        foreach (var cell in _cells.Values)
        {
            LinkedListNode<Entity> entityA = cell.First;
            while (entityA != null)
            {
                // checks surrounding cells of entityA first...
                foreach (var surroundingCellCoord in entityA.Value.SurroundingCells)
                {
                    if (_cells.TryGetValue(surroundingCellCoord, out var surroundingCell))
                    {
                        LinkedListNode<Entity> surroundingEntityB = surroundingCell.First;
                        while (surroundingEntityB != null)
                        {
                            CheckCollision(entityA.Value, surroundingEntityB.Value);
                            surroundingEntityB = surroundingEntityB.Next;
                        }
                    }
                }
                // then checks the cell entityA is in.
                LinkedListNode<Entity> entityB = entityA.Next;
                while (entityB != null)
                {
                    CheckCollision(entityA.Value, entityB.Value);
                    entityB = entityB.Next;
                }
                entityA = entityA.Next;
            }
        }
    }
    

    public void Move(Entity entity)
    {
        Vector2 cellPosition = entity.Position / CellSize;
        cellPosition = Vector2.Floor(cellPosition);
        
        if (cellPosition == entity.CurrentCell) return;

        _cells[entity.CurrentCell].Remove(entity);
        AddEntity(entity);
    }

    public void CheckCollision(Entity entityA, Entity entityB)
    {
        if (entityA.Collision.Intersects(entityB.Collision))
        {
            entityA.HandleCollision(entityB);
            entityB.HandleCollision(entityA);
        }
    }
}