using System.Collections.Generic;
using UnityEngine;

namespace Platformer397
{
    public class GameState : Singleton<GameState>
    {
        [SerializeField] private List<ItemData> inventory = new List<ItemData>();
        [SerializeField] private int rescueCount = 0;
        [SerializeField] private int collectableCount = 0;

        public List<ItemData> GetInventory()
        {
            return inventory;
        }
        public void AddInventory(ItemData item)
        {
            inventory.Add(item);
            if (item == ItemData.CollectableItem)
            {
                collectableCount++;
                EventManager.instance.TriggerUpdateCollectableCount(collectableCount);
            }
        }

        public void RemoveInventory(ItemData item)
        {
            inventory.Remove(item);
            if (item == ItemData.CollectableItem)
            {
                collectableCount--;
                EventManager.instance.TriggerUpdateCollectableCount(collectableCount);
            }
        }

        public int GetRescueCount()
        {
            return rescueCount;
        }

        public void SetRescueCount(int newCount)
        {
            rescueCount = newCount;
            EventManager.instance.TriggerUpdateRescueCount(newCount);

        }
    }
}
