using UnityEngine;
using UnityEngine.SceneManagement;

namespace RollingGround
{
    /// <summary>
    /// シーンの管理を行うクラス
    /// </summary>
    public class MSceneManager : MonoBehaviour
    {
        /// <summary>
        /// シーンの追加読み込み
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadSceneAsync(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        /// <summary>
        /// シーンの削除
        /// </summary>
        /// <param name="sceneName"></param>

        public void CloseSceneAsync(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
