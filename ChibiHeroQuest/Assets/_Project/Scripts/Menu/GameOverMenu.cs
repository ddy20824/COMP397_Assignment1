using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button restartGameBtn;
        [SerializeField] private Button menuGameBtn;
        void Start()
        {
            AudioManager.Instance.PlayGameOverMusic();
            Cursor.lockState = CursorLockMode.None;
            restartGameBtn.onClick.AddListener(() => { SceneController.Instance.ChangeScene("MainScene"); });
            menuGameBtn.onClick.AddListener(() => { SceneController.Instance.ChangeScene("StartMenu"); });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
