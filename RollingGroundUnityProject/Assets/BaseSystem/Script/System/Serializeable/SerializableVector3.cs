using UnityEngine;

/// <summary>
/// Vector3IntをJSONに書き出す形式に変換するクラス
/// </summary>
public class SerializableVector3Int
{
    public int X;
    public int Y;
    public int Z;

    /// <summary>
    /// Vector3IntをSerializableVector3Intに変換するコンストラクタ
    /// </summary>
    /// <param name="pos"></param>
    public SerializableVector3Int(Vector3Int pos)
    {
        X = pos.x;
        Y = pos.y;
        Z = pos.z;
    }

    /// <summary>
    /// SerializableVector3IntをVector3Intに変換するメソッド
    /// </summary>
    /// <returns></returns>
    public Vector3Int ToVector3Int()
    {
        return new Vector3Int(X, Y, Z);
    }
}
