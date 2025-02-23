/*
 * Source File: GameManager.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the audio and is a singleton.
 * 
 * Revision History:
 * - 2025-02-22: Initial version created. Add volume silder and music/sound.
 * - 2025-02-23: Add Game Over music.
 */

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

namespace Platformer397
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] AudioMixer audioMixer;
        public AudioSource audioSource;
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
            PlayMainMenuMusic();

            float musicVolume;
            audioMixer.GetFloat("MusicVolume", out musicVolume);
            musicSlider.SetValueWithoutNotify(musicVolume);

            float soundVolume;
            audioMixer.GetFloat("SoundVolume", out soundVolume);
            soundSlider.SetValueWithoutNotify(soundVolume);
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

        public void MusicSlilderOnClick()
        {
            audioMixer.SetFloat("MusicVolume", musicSlider.value);
        }

        public void SoundSlilderOnClick()
        {
            audioMixer.SetFloat("SoundVolume", soundSlider.value);
        }
    }
}
