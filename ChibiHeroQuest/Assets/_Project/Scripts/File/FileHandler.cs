using System.IO;
using UnityEngine;

namespace Platformer397
{
    public class FileHandler
    {
        private string fileName;
        public FileHandler(string fileName)
        {
            this.fileName = fileName;
        }
        public void Save(GameState data)
        {
            Debug.Log(JsonUtility.ToJson(data));
            data.CastHashSetToArray();
            string json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, fileName);

            File.WriteAllText(path, json);
            Debug.Log("Game Saved at:" + path);
        }

        public void Load()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Debug.Log("File Load:" + json);
                JsonUtility.FromJsonOverwrite(json, GameState.Instance);
                GameState.Instance.CastArrayToSet();
            }
            else
            {
                Debug.Log("File not exist.");
            }
        }
    }
}