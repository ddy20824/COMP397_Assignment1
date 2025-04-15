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
using TMPro;
using UnityEngine;

namespace Platformer397
{
    public class QuestMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] questList;

        void OnEnable()
        {
            var questLists = GameState.Instance.GetAllQuest();
            for (int i = 0; i < questLists.Count; i++)
            {
                questList[i].SetActive(true);
                questList[i].transform.Find("Description").GetComponent<TMP_Text>().text = questLists[i].Name;
                questList[i].transform.Find("CheckBox").GetChild(0).gameObject.SetActive(questLists[i].IsComplete);
                questList[i].transform.Find("Cover").gameObject.SetActive(questLists[i].IsComplete);
            }
        }
    }
}
