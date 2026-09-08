using UnityEngine;

public class MExitGame : MonoBehaviour
{

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
