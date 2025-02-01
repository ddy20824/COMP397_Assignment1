using UnityEngine;

namespace Platformer397
{
    public class MenuController : MonoBehaviour
    {
        public GameObject startMenu;
        public GameObject optionMenu;
        public GameObject pauseMenu;
        public GameObject bagMenu;
        bool optionMenuOpen = false;
        bool pauseMenuOpen = false;
        bool bagMenuOpen = false;

        void Awake()
        {
            startMenu.SetActive(true);
        }
        void Update()
        {
            if (startMenu.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && !bagMenuOpen && !optionMenuOpen)
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
                if (Input.GetKeyDown(KeyCode.I) && !pauseMenuOpen)
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

        public void ExitGame()
        {
            Debug.Log("Exit Game");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
