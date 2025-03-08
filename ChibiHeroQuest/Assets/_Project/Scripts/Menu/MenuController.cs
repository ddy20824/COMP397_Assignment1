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
        public GameObject startMenu;
        public GameObject optionMenu;
        public GameObject pauseMenu;
        public GameObject bagMenu;
        public GameObject mapMenu;
        [SerializeField] private InputReader input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;

        void Awake()
        {
            // Time.timeScale = 0;
            // startMenu.SetActive(true);
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
        void Update()
        {
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
            if (startMenu.activeSelf == false)
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

        public void OpenStartPanel()
        {
            startMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CloseStartPanel()
        {
            startMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenOptionPanel()
        {
            audioSource.PlayOneShot(buttonSound);
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

        public void NewGame()
        {
            audioSource.PlayOneShot(buttonSound);
            CloseStartPanel();
            AudioManager.Instance.PlayGamePlayMusic();
        }

        public void LoadGame()
        {
            audioSource.PlayOneShot(buttonSound);
            CloseStartPanel();
            AudioManager.Instance.PlayGamePlayMusic();
        }

        public void PauseLoadGame()
        {
            audioSource.PlayOneShot(buttonSound);
            ClosePausePanel();
            AudioManager.Instance.PlayGamePlayMusic();
        }

        public void SaveGame()
        {
            audioSource.PlayOneShot(buttonSound);
            ClosePausePanel();
            AudioManager.Instance.PlayGamePlayMusic();
        }

        public void BackToMenu()
        {
            audioSource.PlayOneShot(buttonSound);
            ClosePausePanel();
            OpenStartPanel();
            // AudioManager.Instance.PlayMainMenuMusic();
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
