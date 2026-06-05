using UnityEngine;

public class SerializableVector3Int
{
    public int X;
    public int Y;
    public int Z;

    public SerializableVector3Int(Vector3Int pos)
    {
        X = pos.x;
        Y = pos.y;
        Z = pos.z;
    }

    public Vector3Int ToVector3Int()
    {
        return new Vector3Int(X, Y, Z);
    }
}
