/*
 * Source File: MenuController.cs
 * Author: YuHsuan Chen
 * Student Number: 301448975
 * Date Last Modified: 2025-02-01
 * 
 * Program Description:
 * This program manages the opening and closing of a menu system.
 * It provides functions to toggle the menu state and handle related UI interactions.
 * 
 * Revision History:
 * - 2025-02-01: Initial version created.
 */
using Unity.VisualScripting;
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
        public GameObject endMenu;
        bool optionMenuOpen = false;
        bool pauseMenuOpen = false;
        bool bagMenuOpen = false;
        bool mapMenuOpen = false;

        void Awake()
        {
            startMenu.SetActive(true);
        }
        void Update()
        {
            if (startMenu.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && !bagMenuOpen && !optionMenuOpen && !mapMenuOpen)
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
                if (Input.GetKeyDown(KeyCode.I) && !pauseMenuOpen && !mapMenuOpen)
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
                if (Input.GetKeyDown(KeyCode.M) && !bagMenuOpen && !optionMenuOpen && !pauseMenuOpen)
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

                if (Input.GetKeyDown(KeyCode.G) && !bagMenuOpen && !optionMenuOpen && !pauseMenuOpen && !bagMenuOpen)
                {
                    OpenEndPanel();
                }
            }
        }

        public void OpenStartPanel()
        {
            startMenu.SetActive(true);
        }

        public void CloseStartPanel()
        {
            startMenu.SetActive(false);
        }

        public void OpenOptionPanel()
        {
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
        }

        public void ClosePausePanel()
        {
            pauseMenu.SetActive(false);
            pauseMenuOpen = false;
        }

        public void OpenBagPanel()
        {
            bagMenu.SetActive(true);
            bagMenuOpen = true;
        }

        public void CloseBagPanel()
        {
            bagMenu.SetActive(false);
            bagMenuOpen = false;
        }

        public void OpenMapPanel()
        {
            mapMenu.SetActive(true);
            mapMenuOpen = true;
        }

        public void CloseMapPanel()
        {
            mapMenu.SetActive(false);
            mapMenuOpen = false;
        }

        public void OpenEndPanel()
        {
            endMenu.SetActive(true);
        }

        public void CloseEndPanel()
        {
            endMenu.SetActive(false);
        }

        public void NewGame()
        {
            CloseStartPanel();
            GameManager.Instance.PlayGamePlayMusic();
        }

        public void BackToMenu()
        {
            OpenStartPanel();
            ClosePausePanel();
            GameManager.Instance.PlayMainMenuMusic();
        }

        public void ExitGame()
        {
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
