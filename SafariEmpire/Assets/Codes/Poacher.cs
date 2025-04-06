using UnityEngine;
using Codes.animal;

public class Poacher : Entity
{
    public AnimalGroup targetAnimal;
    public int visionRange;

    public Poacher(Vector2 spawnPosition) : base(spawnPosition)
    {
        this.visionRange = 3;
        
        int rnd = Random.Range(1, 5);
        switch (rnd)
        {
            case 1: this.targetAnimal = new AnimalGroup(new Vector2(0,0), Codes.animal.AnimalType.Hippo); break;
            case 2: this.targetAnimal = new AnimalGroup(new Vector2(0, 0), Codes.animal.AnimalType.Gazella); break;
            case 3: this.targetAnimal = new AnimalGroup(new Vector2(0, 0), Codes.animal.AnimalType.Crocodile); break;
            case 4: this.targetAnimal = new AnimalGroup(new Vector2(0, 0), Codes.animal.AnimalType.Gepard); break;
        }
        this.targetPosition = new Vector2(UnityEngine.Random.Range(spawnPosition.x-visionRange, spawnPosition.x+visionRange+1), UnityEngine.Random.Range(spawnPosition.y-visionRange, spawnPosition.y+visionRange+1));
    }
}
