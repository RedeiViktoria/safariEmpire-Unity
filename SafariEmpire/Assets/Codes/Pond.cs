using UnityEngine;

public class Pond
{
    private int x;
    private int y;
    public Vector2 spawnPosition;

    public GameObject obj;

    public Pond(int x, int y)
    {
        this.spawnPosition = new Vector2(x, y);
    }
}
