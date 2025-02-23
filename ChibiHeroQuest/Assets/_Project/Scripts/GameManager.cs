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
