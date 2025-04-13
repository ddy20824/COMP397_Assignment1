/*
 * Source File: GameState.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-04-04
 * 
 * Program Description:
 * This program manages game data. This is a singleton instance.
 * 
 * Revision History:
 * - 2025-03-07: Initial version created.
 * - 2025-03-08: Add destructibleObject, collactable/rescue count, inventory, enemy and falling blocks status.
 * - 2025-03-09: Add save/load.
 * - 2025-03-12: Add win.
 * - 2025-03-13: Clear inventory when restart.
 * - 2025-04-04: Add Observer pattern
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer397
{
    public class GameState : Singleton<GameState>
    {
        [SerializeField] private List<ItemData> inventory;
        [SerializeField] private int rescueCount = 0;
        [SerializeField] private int collectableCount = 0;
        [SerializeField] private string[] recordChestBoxName;
        [SerializeField] private string[] recordDestuctibleObjectName;
        [SerializeField] private string[] recordEnemyName;
        [SerializeField] private string[] recordFallingGroundName;
        [SerializeField] private Vector3 playerPosition;
        [SerializeField] private int health;
        [SerializeField] private bool isWin = false;
        [SerializeField] private int questIndex;
        private HashSet<string> chestBoxName;
        private HashSet<string> destuctibleObjectName;
        private HashSet<string> enemyName;
        private HashSet<string> fallingGroundName;
        private List<QuestItem> questList;

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
            inventory = new List<ItemData>();
            questList = new List<QuestItem>
            {
                new() {Name= "Use Stick to Move",IsComplete = false},
                new() {Name= "Press Jump Button",IsComplete = false},
                new() {Name= "Open the ChestBox",IsComplete = false},
                new() {Name= "Attack Enemy",IsComplete = false},
                new() {Name= "Rescue the Animal",IsComplete = false},
            };
            questIndex = 0;
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
            inventory = new List<ItemData>();
            questList = new List<QuestItem>
            {
                new() {Name= "Use Stick to Move",IsComplete = false},
                new() {Name= "Press Jump Button",IsComplete = false},
                new() {Name= "Open the ChestBox",IsComplete = false},
                new() {Name= "Attack Enemy",IsComplete = false},
                new() {Name= "Rescue the Animal",IsComplete = false},
            };
            questIndex = 0;
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
            }
        }

        public void RemoveInventory(ItemData item)
        {
            inventory.Remove(item);
            if (item == ItemData.CollectableItem)
            {
                collectableCount--;
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


        public int GetCountByNotifyType(NotifyType type)
        {
            if (type == NotifyType.rescueItem)
                return rescueCount;
            else
                return collectableCount;
        }

        public void SetRescueCount(int newCount)
        {
            rescueCount = newCount;

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

        public void SetIsWin(bool win)
        {
            isWin = win;
        }

        public bool GetIsWin()
        {
            return isWin;
        }

        public int GetQuestIndex()
        {
            return questIndex;
        }

        public List<QuestItem> GetAllQuest()
        {
            return questList;
        }

        public QuestItem GetQuestItem()
        {
            if (questIndex >= questList.Count) { return null; }
            return questList[questIndex];
        }

        public void SetQuestItem()
        {
            questList[questIndex].IsComplete = true;
            questIndex++;
        }

        public void HandleQuestItemStatus()
        {
            int index = 0;
            while (index <= questIndex)
            {
                questList[index].IsComplete = true;
                index++;
            }
        }
    }

    public class QuestItem
    {
        public string Name;
        public bool IsComplete;
    }
}
