using System;
using UnityEngine;

public class Entity
{
    public GameObject obj;

    public Vector2 spawnPosition;

    public Entity(Vector2 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }
}
