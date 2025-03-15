/*
 * Source File: AudioManager.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-03-08
 * 
 * Program Description:
 * This program manages the audio and is a singleton.
 * 
 * Revision History:
 * - 2025-02-22: Initial version created. Add volume silder and music/sound.
 * - 2025-02-23: Add Game Over music.
 * - 2025-03-08: Remove unused function
 */

using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource audioSource;
        public AudioClip mainMenuMusic;
        public AudioClip gamePlayMusic;
        public AudioClip gameOverMusic;
        [SerializeField] Slider musicSlider;
        [SerializeField] Slider soundSlider;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayMainMenuMusic()
        {
            PlayMusic(mainMenuMusic);
        }

        public void PlayGamePlayMusic()
        {
            PlayMusic(gamePlayMusic);
        }

        public void PlayGameOverMusic()
        {
            PlayMusic(gameOverMusic);
        }

        private void PlayMusic(AudioClip clip)
        {
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
