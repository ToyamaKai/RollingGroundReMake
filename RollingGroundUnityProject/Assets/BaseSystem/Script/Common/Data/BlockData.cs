using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class BlockData : ScriptableObject
{
    public BlockID id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}
