/*
 * Source File: DataPersistentManager.cs
 * Author: YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-03-14
 * 
 * Program Description:
 * This program manages data flow in the game.
 * 
 * Revision History:
 * - 2025-03-08: Initial version created. Add Save/Load game and loading scene.
 * - 2025-03-14: Remove unused variable and debug log.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Platformer397
{
    public class DataPersistentManager : Singleton<DataPersistentManager>
    {

        private List<IDataPersistent> dataPersistentObjects;
        private FileHandler fileHandler;

        void Awake()
        {
            base.Awake();
            this.dataPersistentObjects = FindAllDataPersistentObjects();
            this.fileHandler = new FileHandler("Save.sav");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        public void NewGame()
        {
            GameState.Instance.ResetGameState();
            StartCoroutine(DisplayLoadingScreen("MainScene"));
        }

        public void LoadGame()
        {
            fileHandler.Load();
            StartCoroutine(DisplayLoadingScreen("MainScene"));
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            this.dataPersistentObjects = FindAllDataPersistentObjects();
            foreach (IDataPersistent dataPersistent in dataPersistentObjects)
            {
                dataPersistent.LoadData(GameState.Instance);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistent dataPersistent in dataPersistentObjects)
            {
                dataPersistent.SaveData();
            }
            fileHandler.Save(GameState.Instance);
        }

        List<IDataPersistent> FindAllDataPersistentObjects()
        {
            IEnumerable<IDataPersistent> dataPersistents = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistent>();
            return new List<IDataPersistent>(dataPersistents);
        }

        IEnumerator DisplayLoadingScreen(string sceneName)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
            while (!async.isDone)
            {
                EventManager.instance.TriggerLoadingActive();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}