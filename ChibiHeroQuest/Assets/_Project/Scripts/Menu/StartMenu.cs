using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button newGameBtn;
        [SerializeField] private Button loadGameBtn;
        [SerializeField] private Button optionsBtn;
        [SerializeField] private Button ExitBtn;
        [SerializeField] private GameObject optionMenu;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            AudioManager.Instance.PlayMainMenuMusic();
            newGameBtn.onClick.AddListener(NewGame);
            loadGameBtn.onClick.AddListener(LoadGame);
            optionsBtn.onClick.AddListener(Options);
            ExitBtn.onClick.AddListener(ExitGame);
        }

        public void NewGame()
        {
            audioSource.PlayOneShot(buttonSound);
            SceneController.Instance.ChangeScene("MainScene");
        }

        public void LoadGame()
        {
            audioSource.PlayOneShot(buttonSound);
            SceneController.Instance.ChangeScene("MainScene");
        }

        public void Options()
        {
            audioSource.PlayOneShot(buttonSound);
            optionMenu.SetActive(true);
        }

        public void ExitGame()
        {
            audioSource.PlayOneShot(buttonSound);
            Debug.Log("Exit Game");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
