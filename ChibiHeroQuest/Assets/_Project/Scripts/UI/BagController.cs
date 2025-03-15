/*
 * Source File: BagController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-03-08
 * 
 * Program Description:
 * This program manages inventory system display.
 * 
 * Revision History:
 * - 2025-03-08: Initial version created.
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class BagController : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons;
        void OnEnable()
        {
            UpdateBagItem();
        }

        void Clear()
        {
            foreach (var button in buttons)
            {
                button.GetComponent<BagItemController>().Clear();
            }
        }

        public void UpdateBagItem()
        {
            Clear();
            int index = 0;
            var inventoryList = GameState.Instance.GetInventory();

            var healNum = inventoryList.FindAll(x => x == ItemData.HealPosion).Count;
            if (healNum > 0)
            {
                buttons[index].GetComponent<BagItemController>().UpdateItem(ItemData.HealPosion, healNum);
                index++;
            }

            var collectNum = inventoryList.FindAll(x => x == ItemData.CollectableItem).Count;
            if (collectNum > 0)
            {
                buttons[index].GetComponent<BagItemController>().UpdateItem(ItemData.CollectableItem, collectNum);

            }
        }
    }
}
