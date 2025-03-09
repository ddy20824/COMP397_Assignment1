using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer397
{
    public class GameState : Singleton<GameState>
    {
        [SerializeField] private List<ItemData> inventory = new List<ItemData>();
        [SerializeField] private int rescueCount = 0;
        [SerializeField] private int collectableCount = 0;
        [SerializeField] private string[] recordChestBoxName;
        [SerializeField] private string[] recordDestuctibleObjectName;
        [SerializeField] private string[] recordEnemyName;
        [SerializeField] private string[] recordFallingGroundName;
        [SerializeField] private Vector3 playerPosition;
        [SerializeField] private int health;
        private HashSet<string> chestBoxName;
        private HashSet<string> destuctibleObjectName;
        private HashSet<string> enemyName;
        private HashSet<string> fallingGroundName;

        private GameState()
        {
            rescueCount = 0;
            collectableCount = 0;
            playerPosition = new Vector3(-3f, 4f, 20f);
            health = 5;
            chestBoxName = new HashSet<string>();
            destuctibleObjectName = new HashSet<string>();
            enemyName = new HashSet<string>();
            fallingGroundName = new HashSet<string>();
            CastHashSetToArray();
        }

        public void ResetGameState()
        {
            rescueCount = 0;
            collectableCount = 0;
            playerPosition = new Vector3(-3f, 4f, 20f);
            health = 5;
            chestBoxName = new HashSet<string>();
            destuctibleObjectName = new HashSet<string>();
            enemyName = new HashSet<string>();
            fallingGroundName = new HashSet<string>();
        }

        public void UpdateGameUI()
        {
            EventManager.instance.TriggerUpdateCollectableCount(collectableCount);
            EventManager.instance.TriggerUpdateRescueCount(rescueCount);
        }

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

        public int GetCollectableCount()
        {
            return collectableCount;
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

        internal void SetPlayerPosition(Vector3 position)
        {
            playerPosition = position;
        }

        public Vector3 GetPlayerPosition()
        {
            return playerPosition;
        }

        internal void SetPlayerHealth(int playerHealth)
        {
            health = playerHealth;
        }

        public int GetPlayerHealth()
        {
            return health;
        }

        public void CastHashSetToArray()
        {
            recordChestBoxName = chestBoxName.ToArray();
            recordDestuctibleObjectName = destuctibleObjectName.ToArray();
            recordEnemyName = enemyName.ToArray();
            recordFallingGroundName = fallingGroundName.ToArray();
        }

        public void CastArrayToSet()
        {
            chestBoxName = new HashSet<string>(recordChestBoxName);
            destuctibleObjectName = new HashSet<string>(recordDestuctibleObjectName);
            enemyName = new HashSet<string>(recordEnemyName);
            fallingGroundName = new HashSet<string>(recordFallingGroundName);
        }

        public void SetChestBoxName(string name)
        {
            chestBoxName.Add(name);
        }
        public bool CheckChestBoxNameExist(string name)
        {
            return chestBoxName.Contains(name);
        }

        public void SetDestructibleObjectName(string name)
        {
            destuctibleObjectName.Add(name);
        }

        public bool CheckDestructibleObjectNameExist(string name)
        {
            return destuctibleObjectName.Contains(name);
        }

        public void SetEnemyName(string name)
        {
            enemyName.Add(name);
        }

        public bool CheckEnemyNameExist(string name)
        {
            return recordEnemyName.Contains(name);
        }

        public void SetFallingGroundName(string name)
        {
            fallingGroundName.Add(name);
        }

        public bool CheckFallingGroundNameExist(string name)
        {
            return recordFallingGroundName.Contains(name);
        }
    }
}
