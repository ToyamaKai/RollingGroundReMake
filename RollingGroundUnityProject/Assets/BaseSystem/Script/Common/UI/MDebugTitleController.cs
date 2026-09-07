using UnityEngine;
using UnityEngine.SceneManagement;

public class MDebugTitleController : MonoBehaviour
{
    /// <summary>
    /// デバッグステージのロード
    /// </summary>
    public void LoadDebugStage()
    {
        SceneManager.LoadScene("DebugStage01");
    }
}
