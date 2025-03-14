using UnityEngine;

public class Hill
{
    private int x;
    private int y;
    public Vector2 spawnPosition;

    public GameObject obj;

    public Hill(int x, int y)
    {
        this.spawnPosition = new Vector2(x, y);
    }


}
