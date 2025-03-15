using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class BarController : MonoBehaviour
    {
        public Image barImage;
        public float maxValue = 5f;
        private float currentValue;

        void OnEnable()
        {
            EventManager.instance.UpdateHealth += SetValue;
        }

        void OnDisable()
        {
            EventManager.instance.UpdateHealth -= SetValue;
        }

        void Start()
        {
            currentValue = maxValue;
            UpdateBar();
        }

        public void SetValue(int value)
        {
            currentValue = Mathf.Clamp(value, 0, maxValue);
            StartCoroutine(SmoothUpdateBar(currentValue / maxValue));
        }

        private void UpdateBar()
        {
            if (barImage != null)
            {
                barImage.fillAmount = currentValue / maxValue;
            }
        }
        public IEnumerator SmoothUpdateBar(float targetValue)
        {
            float startValue = barImage.fillAmount;
            float time = 0f;
            while (time < 0.5f)
            {
                time += Time.unscaledDeltaTime;
                barImage.fillAmount = Mathf.Lerp(startValue, targetValue, time / 0.5f);
                yield return null;
            }
        }
    }
}
