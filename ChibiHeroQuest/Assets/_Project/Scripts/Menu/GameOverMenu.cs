using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button restartGameBtn;
        [SerializeField] private Button menuGameBtn;
        [SerializeField] private TMP_Text rescueText;
        [SerializeField] private TMP_Text collectableText;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;
        void Start()
        {
            AudioManager.Instance.PlayGameOverMusic();
            Cursor.lockState = CursorLockMode.None;
            restartGameBtn.onClick.AddListener(NewGame);
            menuGameBtn.onClick.AddListener(BackToMenu);
            rescueText.text = GameState.Instance.GetRescueCount().ToString();
            collectableText.text = GameState.Instance.GetCollectableCount().ToString();
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

        public void BackToMenu()
        {
            playButtonSound();
            SceneController.Instance.ChangeScene("StartMenu");
        }
    }
}
