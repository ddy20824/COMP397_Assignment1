using TMPro;
using UnityEngine;

namespace Platformer397
{
    public class CollectUIController : MonoBehaviour
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
