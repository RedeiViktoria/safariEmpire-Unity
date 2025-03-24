using System.Collections.Generic;
using UnityEngine;

public class Path : Entity
{
    public List<Path> neighbors = new List<Path>();
    public Path(Vector2 spawnPosition) : base(spawnPosition)
    {
    }

    public bool IsAdjacent(Path other)
    {
        float gridSize = 1.0f; // The grid size you're using
        float distanceX = Mathf.Abs(spawnPosition.x - other.spawnPosition.x);
        float distanceY = Mathf.Abs(spawnPosition.y - other.spawnPosition.y);

        return distanceX <= gridSize && distanceY <= gridSize && (distanceX == gridSize || distanceY == gridSize);
    }
}
