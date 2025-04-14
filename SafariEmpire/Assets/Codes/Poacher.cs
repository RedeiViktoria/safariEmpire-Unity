using UnityEngine;
using Codes.animal;
using System;

public class Poacher : Entity
{
    public Codes.animal.AnimalType targetAnimal;
    public int visionRange;
    public bool visible;

    public Poacher(Vector2 spawnPosition) : base(spawnPosition)
    {
        this.visionRange = 3;
        this.visible = false;

        int rnd = UnityEngine.Random.Range(1, 5);
        switch (rnd)
        {
            case 1: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Hippo; break;
            case 2: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Gazella; break;
            case 3: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Crocodile; break;
            case 4: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Gepard; break;
        }
        this.targetPosition = new Vector2(UnityEngine.Random.Range(spawnPosition.x - visionRange, spawnPosition.x + visionRange + 1), UnityEngine.Random.Range(spawnPosition.y - visionRange, spawnPosition.y + visionRange + 1));
    }

    public void setVisibility(bool visible)
    {
        var spriteRenderer = this.obj.GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        if (visible)
        {
            color.a = 1f; // 20% látható
        }
        else
        {
            color.a = 0f;
        }
        spriteRenderer.color = color;
    }
}
