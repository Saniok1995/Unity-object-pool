using System;

public class GameData
{
    public int ObjectCount { get;}
    public float BoardSize => (float) Math.Sqrt(ObjectCount);
    public float CentralPosition => BoardSize / 2;

    public GameData(int objectCount)
    {
        ObjectCount = objectCount;
    }
}
