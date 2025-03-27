/*
 * Source File: OptionsMeStartMenunu.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-03-09
 * 
 * Program Description:
 * This program manages start menu system.
 * 
 * Revision History:
 * - 2025-03-07: Initial version created.
 * - 2025-03-09: Add loading page.
 */

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
#if !UNITY_ANDROID
            Cursor.lockState = CursorLockMode.None;
#endif
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
