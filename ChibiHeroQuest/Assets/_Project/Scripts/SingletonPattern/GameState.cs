using UnityEngine;

namespace Platformer397
{
    public class GameState : Singleton<GameState>
    {
        private float musicVolume;
        private float soundVolume;

        public void SetMusicVolume(float value)
        {
            musicVolume = value;
        }

        public float GetMusicVolume()
        {
            return musicVolume;
        }

        public void SetSoundVolume(float value)
        {
            soundVolume = value;
        }

        public float GetSoundVolume()
        {
            return soundVolume;
        }
    }
}
