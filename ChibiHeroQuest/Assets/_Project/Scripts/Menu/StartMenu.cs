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
            Cursor.lockState = CursorLockMode.None;
            AudioManager.Instance.PlayMainMenuMusic();
            newGameBtn.onClick.AddListener(NewGame);
            loadGameBtn.onClick.AddListener(LoadGame);
            optionsBtn.onClick.AddListener(Options);
            ExitBtn.onClick.AddListener(ExitGame);
        }

        private void playButtonSound()
        {
            audioSource.PlayOneShot(buttonSound);
        }

        public void NewGame()
        {
            playButtonSound();
            DataPersistentManager.Instance.NewGame();
        }

        public void LoadGame()
        {
            playButtonSound();
            DataPersistentManager.Instance.LoadGame();
        }

        public void Options()
        {
            playButtonSound();
            optionMenu.SetActive(true);
        }

        public void ExitGame()
        {
            playButtonSound();
            Debug.Log("Exit Game");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
