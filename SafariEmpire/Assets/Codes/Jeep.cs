using Codes.animal;
using System.Collections.Generic;
using UnityEngine;

public class Jeep : Entity
{
    public List<Path> path = new List<Path>();
    public List<AnimalGroup> encounteredAnimals = new List<AnimalGroup>();
    public int idx = 0;
    public bool moving = false;
    public bool isFinished = false; 
    public Jeep(Vector2 spawnPosition) : base(spawnPosition)
    {
        
    }

    public void chooseRandomPath(List<List<Path>> paths)
    {
        path = paths[UnityEngine.Random.Range(0, paths.Count)];
    }
    public bool move()
    {
        if (this.idx < path.Count)
        {
            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, path[this.idx].obj.transform.position, 2.0f * Time.deltaTime);
            if (this.obj.transform.position == path[this.idx].obj.transform.position)
            {
                this.idx += 1;
            }
            return false;
        }
        else
        {
            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, this.spawnPosition, 2.0f * Time.deltaTime);
            if ((Vector2)this.obj.transform.position == this.spawnPosition)
            {
                moving = false;
                this.idx = 0;

                //biztosítja, hogy csak 1szer számolja, amikor beér
                if (!isFinished)
                {
                    isFinished = true;
                    return true;
                }

                return false;
            }
            return false;
        }

    }
}
