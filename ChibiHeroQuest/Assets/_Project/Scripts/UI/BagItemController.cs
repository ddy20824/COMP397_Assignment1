/*
 * Source File: BagItemController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-03-08
 * 
 * Program Description:
 * This program manages using item in bag.
 * 
 * Revision History:
 * - 2025-03-08: Initial version created.
 */
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class BagItemController : MonoBehaviour
    {
        [SerializeField] private Sprite healSprite;
        [SerializeField] private Sprite CollectSprite;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip usedSound;
        private ItemData itemType = ItemData.None;
        private int itemNum = 0;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(UseItem);
        }
        public void Clear()
        {
            itemType = ItemData.None;
            itemNum = 0;
            transform.Find("NumText").gameObject.SetActive(false);
            transform.Find("Item").gameObject.SetActive(false);
        }

        public void UpdateItem(ItemData type, int number)
        {
            itemType = type;
            itemNum = number;

            var numText = transform.Find("NumText");
            numText.gameObject.SetActive(true);
            numText.GetComponent<TextMeshProUGUI>().text = itemNum.ToString();

            var item = transform.Find("Item");
            item.gameObject.SetActive(true);
            item.GetComponent<Image>().sprite = (type == ItemData.HealPosion) ? healSprite : CollectSprite;
        }

        void UseItem()
        {
            if (itemType == ItemData.HealPosion)
            {
                audioSource.PlayOneShot(usedSound);
                EventManager.instance.TriggerHeal();
                itemNum--;
                if (itemNum == 0)
                {
                    transform.parent.GetComponent<BagController>().UpdateBagItem();
                }
                else
                {
                    transform.Find("NumText").GetComponent<TextMeshPro>().text = itemNum.ToString();
                }
            }
        }
    }
}
