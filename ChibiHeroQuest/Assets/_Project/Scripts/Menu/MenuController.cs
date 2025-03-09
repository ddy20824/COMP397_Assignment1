/*
 * Source File: MenuController.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the opening and closing of a menu system.
 * It provides functions to toggle the menu state and handle related UI interactions.
 * 
 * Revision History:
 * - 2025-02-01: Initial version created.
 * - 2025-02-22: Add Music/Sound Slider
 * - 2025-02-23: Add Cursor Lock/None, HandleMap/HandleBag/HandlePause, buttonSound
 */
using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class MenuController : MonoBehaviour
    {
        public GameObject optionMenu;
        public GameObject pauseMenu;
        public GameObject bagMenu;
        public GameObject mapMenu;
        [SerializeField] private InputReader input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;

        void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            EventManager.instance.ShowGameOver += GameOver;
        }
        void Start()
        {
            AudioManager.Instance.PlayGamePlayMusic();
        }

        void OnEnable()
        {
            input.Map += HandleMap;
            input.Bag += HandleBag;
            input.Pause += HandlePause;
        }

        private void OnDisable()
        {
            input.Map -= HandleMap;
            input.Bag -= HandleBag;
            input.Pause -= HandlePause;
        }

        public void HandleMap()
        {
            if (!IsBagMenuActive() && !IsOptionMenuActive() && !IsPauseMenuActive())
            {
                if (!IsMapMenuActive())
                {
                    OpenMapPanel();
                }
                else
                {
                    CloseMapPanel();
                }
            }
        }

        public void HandleBag()
        {
            if (!IsPauseMenuActive() && !IsMapMenuActive())
            {
                if (!IsBagMenuActive())
                {
                    OpenBagPanel();
                }
                else
                {
                    CloseBagPanel();
                }
            }
        }

        public void HandlePause()
        {
            if (!IsBagMenuActive() && !IsOptionMenuActive() && !IsMapMenuActive())
            {
                if (!IsPauseMenuActive())
                {
                    OpenPausePanel();
                }
                else
                {
                    ClosePausePanel();
                }
            }
        }

        public void OpenOptionPanel()
        {
            playButtonSound();
            optionMenu.SetActive(true);
        }

        public void OpenPausePanel()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void ClosePausePanel()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenBagPanel()
        {
            bagMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CloseBagPanel()
        {
            bagMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenMapPanel()
        {
            mapMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CloseMapPanel()
        {
            mapMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void GameOver()
        {
            SceneController.Instance.ChangeScene("GameOver");
        }

        public void LoadGame()
        {
            playButtonSound();
            DataPersistentManager.Instance.LoadGame();
            ClosePausePanel();
            AudioManager.Instance.PlayGamePlayMusic();
        }

        public void SaveGame()
        {
            playButtonSound();
            DataPersistentManager.Instance.SaveGame();
            ClosePausePanel();
            AudioManager.Instance.PlayGamePlayMusic();
        }

        public void BackToMenu()
        {
            playButtonSound();
            ClosePausePanel();
            SceneController.Instance.ChangeScene("StartMenu");
        }

        private void playButtonSound()
        {
            audioSource.PlayOneShot(buttonSound);
        }

        private bool IsOptionMenuActive()
        {
            return optionMenu.activeSelf;
        }

        private bool IsBagMenuActive()
        {
            return bagMenu.activeSelf;
        }

        private bool IsMapMenuActive()
        {
            return mapMenu.activeSelf;
        }

        private bool IsPauseMenuActive()
        {
            return pauseMenu.activeSelf;
        }
    }
}
