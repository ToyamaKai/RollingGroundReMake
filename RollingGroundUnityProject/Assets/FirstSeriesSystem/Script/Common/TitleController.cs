using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    /// <summary>
    /// 新規ゲーム開始ボタンが押されたときに呼ばれるメソッド
    /// </summary>
    public void LoadNewGame()
    {
        SceneManager.LoadScene("");
    }

    public void LoadCreativeMode()
    {

    }
}
