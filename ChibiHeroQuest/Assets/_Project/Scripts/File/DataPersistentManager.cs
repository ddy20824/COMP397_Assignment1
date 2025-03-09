using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

namespace Platformer397
{
    public class DataPersistentManager : Singleton<DataPersistentManager>
    {

        private List<IDataPersistent> dataPersistentObjects;
        private FileHandler fileHandler;
        private GameState gameState;

        // Start is called before the first frame update
        void Awake()
        {
            base.Awake();
            this.dataPersistentObjects = FindAllDataPersistentObjects();
            this.fileHandler = new FileHandler("Save.sav");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        private void OnDestroy()
        {
            // 取消訂閱，避免記憶體洩漏
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
            // 確保場景已加載完畢，可以安全地操作場景中的物件
            Debug.Log(scene.name + " Loaded");
            this.dataPersistentObjects = FindAllDataPersistentObjects();
            foreach (IDataPersistent dataPersistent in dataPersistentObjects)
            {
                dataPersistent.LoadData(GameState.Instance);
            }
            GameState.Instance.UpdateGameUI();
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