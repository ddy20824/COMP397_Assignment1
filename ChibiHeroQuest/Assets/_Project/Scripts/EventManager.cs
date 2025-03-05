using System;
using UnityEngine;

namespace Platformer397
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;
        public event Action LoadingActiveEvent;

        public event Action<int> UpdateHealth;
        public event Action ShowGameOver;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void TriggerUpdateHealth(int health)
        {
            UpdateHealth?.Invoke(health);
        }

        public void TriggerShowGameOver()
        {
            ShowGameOver?.Invoke();
        }
    }
}
