/*
 * Source File: ChestController.cs
 * Author: Sylker Teles, YuHsuan Chen
 * Student Number: 301448975
 * Date Last Modified: 2025-04-04
 * 
 * Program Description:
 * This program manages chests interact
 * Reference from the assets from © 2019 Flying Saci Game Studio
 * 
 * Revision History:
 * - 2025-03-07: Initial version created. Open by iteract input.
 * - 2025-03-09: Update collactable count
 * - 2025-03-09: Add save data and update status when loading
 * - 2025-03-12: Add open sound
 * - 2025-03-13: Add chestbox item display when opening and add chest mass
 * - 2025-03-14: Adjust show chestbox content time
 * - 2025-03-28: Changed to be touch based
 * - 2025-04-04: Add Observer pattern
 */

using UnityEngine;

namespace Platformer397
{
    public class ChestController : MonoBehaviour, IDataPersistent
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private bool isOpen { get; set; }
        [SerializeField] private ItemData chestContent;
        [SerializeField] private InputReader input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;
        private PlayerController player;
        private bool isPlayerAround;
        private Animator animator;
        private GameObject chestDisplayContent;
        private float contentDisplayTime = 2f;

        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            chestDisplayContent = findChildByTag(transform, "ChestContent");
            input.EnablePlayerActions();
        }

        public void Open()
        {
            if (!isOpen)
            {
                if (GameState.Instance.GetQuestIndex() == 2)
                {
                    player.GetComponent<PlayerController>().NotifyObservers(ObserverType.Quest);
                }
                isOpen = true;
                animator.Play("Open");
                audioSource.PlayOneShot(openSound);
                GameState.Instance.AddInventory(chestContent);
                GameState.Instance.SetChestBoxName(name);
                if (chestContent == ItemData.CollectableItem)
                {
                    player.NotifyObservers(ObserverType.Achievement);
                }

                if (chestDisplayContent != null)
                {
                    StartCoroutine(Helper.Delay(ShowChestDisplayContent, 0.3f));
                }
            }
        }


        public void Close()
        {
            if (isOpen)
            {
                isOpen = false;
                animator.Play("Close");
                audioSource.PlayOneShot(closeSound);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == playerTag)
            {
                Open();
            }
        }


        private void ShowChestDisplayContent()
        {
            chestDisplayContent.SetActive(true);
            StartCoroutine(Helper.Delay(() => { chestDisplayContent.SetActive(false); }, contentDisplayTime));
        }

        private GameObject findChildByTag(Transform parent, string inputTag)
        {
            GameObject childWithTag = null;
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).CompareTag(inputTag))
                {
                    childWithTag = parent.GetChild(i).gameObject;
                    break;
                }
            }

            return childWithTag;
        }

        public void LoadData(GameState data)
        {
            if (GameState.Instance.CheckChestBoxNameExist(name))
            {
                isOpen = true;
                if (animator == null)
                    animator = GetComponent<Animator>();
                animator.Play("Open");
            }
        }

        public void SaveData()
        {
        }
    }
}
