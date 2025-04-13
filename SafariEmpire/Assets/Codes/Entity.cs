using System;
using UnityEngine;

public class Entity
{
    public GameObject obj;

    public Vector2 spawnPosition;
    public Vector2 targetPosition;




    public Entity(Vector2 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }
    public Vector2 Position
    { 
        get => this.obj.transform.position;
    }
}
