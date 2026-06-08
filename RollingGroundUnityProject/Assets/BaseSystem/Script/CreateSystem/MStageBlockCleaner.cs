using UnityEngine;

/// <summary>
/// ステージブロックのクリーナークラス
/// </summary>
public class MStageBlockCleaner : MonoBehaviour
{
    [SerializeField]
    private Transform m_stageroot;

    public void CleanStageObject()
    {
        foreach(Transform childrenObject in m_stageroot)
        {
            GameObject.Destroy(childrenObject.gameObject); ;
        }
    }
}
