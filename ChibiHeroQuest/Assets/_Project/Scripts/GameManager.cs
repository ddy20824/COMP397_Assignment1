using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace Platformer397
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public AudioSource audioSource;
        public AudioClip mainMenuMusic;
        public AudioClip gamePlayMusic;

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
            PlayMainMenuMusic(); // 遊戲啟動時播放主選單音樂
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
    }
}
