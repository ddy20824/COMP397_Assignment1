using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class BagItemController : MonoBehaviour
    {
        [SerializeField] private Sprite healSprite;
        [SerializeField] private Sprite CollectSprite;
        private ItemData itemType = ItemData.None;
        private int itemNum = 0;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
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
