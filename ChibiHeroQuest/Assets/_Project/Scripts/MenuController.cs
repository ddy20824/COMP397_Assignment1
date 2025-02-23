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

namespace Platformer397
{
    public class MenuController : MonoBehaviour
    {
        public GameObject startMenu;
        public GameObject optionMenu;
        public GameObject pauseMenu;
        public GameObject bagMenu;
        public GameObject mapMenu;
        public GameObject endMenu;
        bool optionMenuOpen = false;
        bool pauseMenuOpen = false;
        bool bagMenuOpen = false;
        bool mapMenuOpen = false;
        [SerializeField] private InputReader input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;

        void Awake()
        {
            Time.timeScale = 0;
            startMenu.SetActive(true);
        }

        void OnEnable()
        {
            input.Map += HandleMap;
            input.Bag += HandleBag;
            input.Pause += HandlePause;
        }
        void Update()
        {
            if (startMenu.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.G) && !bagMenuOpen && !optionMenuOpen && !pauseMenuOpen && !bagMenuOpen)
                {
                    OpenEndPanel();
                }
            }
        }

        public void HandleMap()
        {
            if (startMenu.activeSelf == false)
            {
                if (!bagMenuOpen && !optionMenuOpen && !pauseMenuOpen)
                {
                    if (!mapMenuOpen)
                    {
                        OpenMapPanel();
                    }
                    else
                    {
                        CloseMapPanel();
                    }
                }
            }
        }

        public void HandleBag()
        {
            if (startMenu.activeSelf == false)
            {
                if (!pauseMenuOpen && !mapMenuOpen)
                {
                    if (!bagMenuOpen)
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
            if (startMenu.activeSelf == false)
            {
                if (!bagMenuOpen && !optionMenuOpen && !mapMenuOpen)
                {
                    if (!pauseMenuOpen)
                    {
                        OpenPausePanel();
                    }
                    else
                    {
                        ClosePausePanel();
                    }
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
            optionMenuOpen = true;
        }

        public void CloseOptionPanel()
        {
            optionMenu.SetActive(false);
            optionMenuOpen = false;
        }
        public void OpenPausePanel()
        {
            pauseMenu.SetActive(true);
            pauseMenuOpen = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void ClosePausePanel()
        {
            pauseMenu.SetActive(false);
            pauseMenuOpen = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenBagPanel()
        {
            bagMenu.SetActive(true);
            bagMenuOpen = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CloseBagPanel()
        {
            bagMenu.SetActive(false);
            bagMenuOpen = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenMapPanel()
        {
            mapMenu.SetActive(true);
            mapMenuOpen = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CloseMapPanel()
        {
            mapMenu.SetActive(false);
            mapMenuOpen = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenEndPanel()
        {
            endMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CloseEndPanel()
        {
            endMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void NewGame()
        {
            audioSource.PlayOneShot(buttonSound);
            CloseStartPanel();
            GameManager.Instance.PlayGamePlayMusic();
        }

        public void LoadGame()
        {
            audioSource.PlayOneShot(buttonSound);
            CloseStartPanel();
            GameManager.Instance.PlayGamePlayMusic();
        }

        public void PauseLoadGame()
        {
            audioSource.PlayOneShot(buttonSound);
            ClosePausePanel();
            GameManager.Instance.PlayGamePlayMusic();
        }

        public void SaveGame()
        {
            audioSource.PlayOneShot(buttonSound);
            ClosePausePanel();
            GameManager.Instance.PlayGamePlayMusic();
        }

        public void BackToMenu()
        {
            audioSource.PlayOneShot(buttonSound);
            ClosePausePanel();
            OpenStartPanel();
            GameManager.Instance.PlayMainMenuMusic();
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

        public void MusicSliderOnClick()
        {
            GameManager.Instance.MusicSlilderOnClick();
        }

        public void SoundSlilderOnClick()
        {
            GameManager.Instance.SoundSlilderOnClick();
        }
    }
}
