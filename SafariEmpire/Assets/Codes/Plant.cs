using UnityEngine;

public class Plant : Entity
{
    protected int value;
    protected int grow_rate;
    protected int max_growth;

    public Plant(int value, int rate, Vector2 spawnPosition) : base(spawnPosition)
    {
        this.value = value;
        this.grow_rate = rate;
        this.max_growth = value;
    }

    public void Grow()
    {
        if (value < max_growth) value += grow_rate;
        else value = max_growth;
    }
}

//the value/grow_rate are just examples, changeable for balance
public class Tree : Plant
{
    public Tree(Vector2 spawnPosition) : base(100, 7, spawnPosition) { }
}

public class Bush : Plant
{
    public Bush(Vector2 spawnPosition) : base(70, 5, spawnPosition) { }
}

public class Grass : Plant
{
    public Grass(Vector2 spawnPosition) : base(50, 3, spawnPosition) { }
}
