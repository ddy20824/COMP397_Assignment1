/*
 * Source File: GametUIController.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-03-08
 * 
 * Program Description:
 * This program manages manage game ui - collect count and rescue count.
 * 
 * Revision History:
 * - 2025-03-08: Initial version created.
 */
using TMPro;
using UnityEngine;

namespace Platformer397
{
    public class GametUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text rescueText;
        [SerializeField] private TMP_Text collectableText;

        private void OnEnable()
        {
            EventManager.instance.UpdateRescueCount += UpdateRescueText;
            EventManager.instance.UpdateCollectableCount += UpdateCollectableText;
        }

        private void OnDisable()
        {
            EventManager.instance.UpdateRescueCount -= UpdateRescueText;
            EventManager.instance.UpdateCollectableCount -= UpdateCollectableText;
        }

        private void UpdateRescueText(int rescueCount)
        {
            rescueText.text = rescueCount.ToString();
        }

        private void UpdateCollectableText(int collectableCount)
        {
            collectableText.text = collectableCount.ToString();
        }
    }
}
