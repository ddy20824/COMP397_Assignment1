using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Platformer397
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] AudioMixer audioMixer;
        [SerializeField] Slider musicSlider;
        [SerializeField] Slider soundSlider;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonSound;

        void Start()
        {
            if (musicSlider != null)
            {
                float musicVolume;
                audioMixer.GetFloat("MusicVolume", out musicVolume);
                musicSlider.SetValueWithoutNotify(musicVolume);
            }

            if (soundSlider != null)
            {
                float soundVolume;
                audioMixer.GetFloat("SoundVolume", out soundVolume);
                soundSlider.SetValueWithoutNotify(soundVolume);
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

        public void CloseOptionPanel()
        {
            gameObject.SetActive(false);
            playButtonSound();
        }

        private void playButtonSound()
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
