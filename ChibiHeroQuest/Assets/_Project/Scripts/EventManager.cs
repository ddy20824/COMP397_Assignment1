/*
 * Source File: EventManager.cs
 * Author: Chiayi Lin, YuHsuan Chen
 * Student Number: 301448962, 301448975
 * Date Last Modified: 2025-03-09
 * 
 * Program Description:
 * This program manages events.
 * 
 * Revision History:
 * - 2025-03-04: Initial version created.
 * - 2025-03-07: Add TriggerAddInventory.
 * - 2025-03-08: Add TriggerUpdateRescueCount, TriggerUpdateCollectableCount and TriggerHeal
 * - 2025-03-09: Add TriggerLoadingActive.
 */
using System;
using UnityEngine;

namespace Platformer397
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;
        public event Action LoadingActiveEvent;
        public event Action<int> UpdateHealth;
        public event Action<int> UpdateRescueCount;
        public event Action<int> UpdateCollectableCount;
        public event Action<ItemData> AddInventory;
        public event Action ShowGameOver;
        public event Action PlayerHeal;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void TriggerUpdateHealth(int health)
        {
            UpdateHealth?.Invoke(health);
        }

        public void TriggerUpdateRescueCount(int rescueCount)
        {
            UpdateRescueCount?.Invoke(rescueCount);
        }

        public void TriggerUpdateCollectableCount(int collectableCount)
        {
            UpdateCollectableCount?.Invoke(collectableCount);
        }

        public void TriggerAddInventory(ItemData newItem)
        {
            AddInventory?.Invoke(newItem);
        }

        public void TriggerShowGameOver()
        {
            ShowGameOver?.Invoke();
        }
        public void TriggerHeal()
        {
            PlayerHeal?.Invoke();
        }

        public void TriggerLoadingActive()
        {
            LoadingActiveEvent?.Invoke();
        }
    }
}
