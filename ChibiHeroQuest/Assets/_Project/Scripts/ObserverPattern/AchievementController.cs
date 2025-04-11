/*
 * Source File: AchievementController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-04-04
 * 
 * Program Description:
 * This program manages achievement system.
 * 
 * Revision History:
 * - 2025-04-04: Initial version created.
 */
using UnityEngine;
using TMPro;

namespace Platformer397
{
    public class AchievementController : MonoBehaviour, IObserver, IDataPersistent
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject notify;
        [SerializeField] private NotifyType notifyType;

        private PlayerController player;
        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            player.AddObserver(this, ObserverType.Achievement);
        }
        private void OnDisable()
        {
            player.RemoveObserver(this, ObserverType.Achievement);
        }

        public void OnNotify()
        {
            var count = GameState.Instance.GetCountByNotifyType(notifyType);
            text.text = count.ToString();
            if (count == 3)
            {
                notify.SetActive(true);
                StartCoroutine(Helper.Delay(() => { notify.SetActive(false); }, 1.5f));
            }
        }

        public void LoadData(GameState data)
        {
            var count = GameState.Instance.GetCountByNotifyType(notifyType);
            text.text = count.ToString();
        }

        public void SaveData()
        {
        }
    }

    public enum NotifyType
    {
        rescueItem,
        CollectableItem
    }
}
