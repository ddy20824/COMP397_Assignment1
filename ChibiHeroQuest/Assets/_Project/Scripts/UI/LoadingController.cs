/*
 * Source File: LoadongController.cs
 * Author: YuHsuan Chen
 * Student Number: 301448975
 * Date Last Modified: 2025-03-14
 * 
 * Program Description:
 * This program manages loading page.
 * 
 * Revision History:
 * - 2025-03-08: Initial version created.
 * - 2025-03-14: Fix event not unbind bug.
 */
using UnityEngine;
using TMPro;

namespace Platformer397
{
    public class LoadongController : MonoBehaviour
    {
        public GameObject loadingCanvas;
        public TMP_Text LoadingProgressText;

        private string fullText = "Loading...";

        void OnEnable()
        {
            EventManager.instance.LoadingActiveEvent += DisplayLoadingProgress;
        }

        void OnDisable()
        {
            EventManager.instance.LoadingActiveEvent -= DisplayLoadingProgress;
        }

        void DisplayLoadingProgress()
        {
            if (!loadingCanvas.activeSelf)
            {
                loadingCanvas.SetActive(true);
            }
            int nowTextLength = LoadingProgressText.text.Length;
            if (nowTextLength == fullText.Length)
            {
                LoadingProgressText.text = "Loading";
            }
            else if (nowTextLength < fullText.Length)
            {
                LoadingProgressText.text += fullText[nowTextLength];
            }

        }
    }
}
