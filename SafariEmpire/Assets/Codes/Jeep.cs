using System.Collections.Generic;
using UnityEngine;

public class Jeep : Entity
{
    public List<Path> path = new List<Path>();
    public int idx = 0;
    public bool moving = false;
    public Jeep(Vector2 spawnPosition) : base(spawnPosition)
    {
        
    }

    public void chooseRandomPath(List<List<Path>> paths)
    {
        path = paths[UnityEngine.Random.Range(0, paths.Count)];
    }
    public void move()
    {
        if (idx < path.Count)
        {
            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, path[idx].obj.transform.position, 2.0f * Time.deltaTime);
            if (this.obj.transform.position == path[idx].obj.transform.position)
            {
                idx += 1;
            }
        }
    }
}
