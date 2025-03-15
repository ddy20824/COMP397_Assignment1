/*
 * Source File: GameOverMenu.cs
 * Author: YuHsuan Chen, Chiayi Lin, Jen MacDonald
 * Student Number: 301448975, 301448962, 301000349
 * Date Last Modified: 2025-03-13
 * 
 * Program Description:
 * This program is manage the game over scene.
 * 
 * Revision History:
 * - 2025-03-07: Initial version created.
 * - 2025-03-09: Add new game function and button sound.
 * - 2025-03-12: Add subtitle and win/lose condition.
 * - 2025-03-13: Edit title and display.
 */

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
        [SerializeField] private GameObject winTitle;
        [SerializeField] private GameObject loseTitle;
        [SerializeField] private TMP_Text subTitle;
        void Start()
        {
            AudioManager.Instance.PlayGameOverMusic();
            Cursor.lockState = CursorLockMode.None;
            restartGameBtn.onClick.AddListener(NewGame);
            menuGameBtn.onClick.AddListener(BackToMenu);
            rescueText.text = GameState.Instance.GetRescueCount().ToString();
            collectableText.text = GameState.Instance.GetCollectableCount().ToString();

            var isWin = GameState.Instance.GetIsWin();
            winTitle.SetActive(isWin);
            loseTitle.SetActive(!isWin);

            if (GameState.Instance.GetCollectableCount() == 3 && GameState.Instance.GetRescueCount() == 3)
            {
                subTitle.text = "You completed all missions!";
            }
            else
            {
                subTitle.text = "You missed something...";
            }
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
