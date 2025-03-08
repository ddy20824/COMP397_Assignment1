using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer397
{
    public class SceneController : Singleton<SceneController>
    {
        public void ChangeScene(string sceneName)
        {
            Debug.Log("ChangeScene");
            SceneManager.LoadScene(sceneName);
        }
    }
}
