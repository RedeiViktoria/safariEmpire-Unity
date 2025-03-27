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
        float gridSize = 1.0f; // A rács mérete
        float distanceX = Mathf.Abs(spawnPosition.x - other.spawnPosition.x);
        float distanceY = Mathf.Abs(spawnPosition.y - other.spawnPosition.y);

        // Csak akkor igaz, ha pontosan egy tengelyen tér el gridSize távolsággal
        return (distanceX == gridSize && distanceY == 0) || (distanceY == gridSize && distanceX == 0);
    }
}
