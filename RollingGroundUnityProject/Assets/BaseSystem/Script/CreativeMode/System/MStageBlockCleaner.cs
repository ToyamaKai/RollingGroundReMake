using UnityEngine;

/// <summary>
/// ステージブロックのクリーナークラス
/// TODO: これは実処理に書き出して、使い回せるようにしておくべきかもしれない。また、Dictionaryのクリーンも行うようにする。
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
