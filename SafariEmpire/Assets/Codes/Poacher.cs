using UnityEngine;
using Codes.animal;

public class Poacher : Entity
{
    public Codes.animal.AnimalType targetAnimal;
    public int visionRange;

    public Poacher(Vector2 spawnPosition) : base(spawnPosition)
    {
        this.visionRange = 3;
        
        int rnd = Random.Range(1, 5);
        switch (rnd)
        {
            case 1: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Hippo; break;
            case 2: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Gazella; break;
            case 3: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Crocodile; break;
            case 4: this.targetAnimal = this.targetAnimal = Codes.animal.AnimalType.Gepard; break;
        }
    }
}
