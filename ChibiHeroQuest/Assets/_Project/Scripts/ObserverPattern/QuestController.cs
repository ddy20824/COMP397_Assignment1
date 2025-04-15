/*
 * Source File: QuestMenu.cs
 * Author: Class example, Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-04-11
 * 
 * Program Description:
 * This program manages quest menu.
 * 
 * Revision History:
 * - 2025-04-11: Initial version created.
 */
using System.Collections;
using TMPro;
using UnityEngine;

namespace Platformer397
{
    public class QuestController : MonoBehaviour, IObserver, IDataPersistent
    {
        [SerializeField] private GameObject quest;
        [SerializeField] private GameObject description;
        [SerializeField] private GameObject condition;
        private PlayerController player;
        private RectTransform questPanel;
        private float scaleUpTime = 0.2f;
        private float slideOutTime = 0.5f;
        private float slideInTime = 0.5f;
        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            questPanel = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            player.AddObserver(this, ObserverType.Quest);
        }

        private void OnDisable()
        {
            player.RemoveObserver(this, ObserverType.Quest);
        }

        public void OnNotify()
        {
            condition.GetComponent<TMP_Text>().text = "(1/1)";
            condition.GetComponent<TMP_Text>().color = Color.green;
            GameState.Instance.SetQuestItem();
            StartCoroutine(PlayQuestTransition());
        }

        public void LoadData(GameState data)
        {
            GameState.Instance.HandleQuestItemStatus();
            if (data.GetQuestItem() == null)
            {
                quest.SetActive(false);
            }
            else
            {
                description.GetComponent<TMP_Text>().text = data.GetQuestItem().Name;
            }
        }

        public void SaveData()
        {
        }

        private IEnumerator PlayQuestTransition()
        {
            yield return new WaitForSeconds(1.0f);

            Vector3 originalScale = questPanel.localScale;
            Vector3 targetScale = originalScale * 1.2f;
            float t = 0f;
            while (t < scaleUpTime)
            {
                t += Time.deltaTime;
                questPanel.localScale = Vector3.Lerp(originalScale, targetScale, t / scaleUpTime);
                yield return null;
            }

            Vector2 originalPos = questPanel.anchoredPosition;
            Vector2 targetPos = originalPos + new Vector2(Screen.width + 300, 0);
            t = 0f;
            while (t < slideOutTime)
            {
                t += Time.deltaTime;
                questPanel.anchoredPosition = Vector2.Lerp(originalPos, targetPos, t / slideOutTime);
                yield return null;
            }

            var newQuest = GameState.Instance.GetQuestItem();
            if (newQuest != null)
            {
                description.GetComponent<TMP_Text>().text = newQuest.Name;
            }
            else
            {
                quest.SetActive(false);
            }
            condition.GetComponent<TMP_Text>().text = "(0/1)";
            condition.GetComponent<TMP_Text>().color = Color.red;
            // questPanel.anchoredPosition = new Vector2(-Screen.width - 300, originalPos.y);
            questPanel.localScale = originalScale;

            t = 0f;
            while (t < slideInTime)
            {
                t += Time.deltaTime;
                questPanel.anchoredPosition = Vector2.Lerp(questPanel.anchoredPosition, originalPos, t / slideInTime);
                yield return null;
            }
        }
    }
}
