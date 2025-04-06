using UnityEngine;

public class Ranger : Entity
{
    public Codes.animal.AnimalType targetAnimal; //csak lekérdezésnél van használva, ha a target>0
    public int target; //ha 0 akkor poacher, ha 1 akkor cheetah ha 2 akkor crocodile
    public int visionRange;
    public Ranger(Vector2 spawnPosition) : base(spawnPosition)
    {
        this.target = 0;
        this.visionRange = 3;
    }

    public void toggleTarget()
    {
        switch (this.target)
        {
            case 0: this.target = 1; this.targetAnimal = Codes.animal.AnimalType.Gepard; break;
            case 1: this.target = 2; this.targetAnimal = Codes.animal.AnimalType.Crocodile; break;
            case 2: this.target = 0; break;
        }
    }
}
