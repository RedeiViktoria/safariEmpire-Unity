using UnityEngine;

public class Ranger : Entity
{
    public string target;
    public int visionRange;
    public Ranger(Vector2 spawnPosition) : base(spawnPosition)
    {
        this.target = "poacher";
        this.visionRange = 3;
    }

    public void toggleTarget()
    {
        switch (this.target)
        {
            case "poacher": this.target = "cheetah"; break;
            case "cheetah": this.target = "crocodile"; break;
            case "crocodile": this.target = "poacher"; break;
        }
    }
}
