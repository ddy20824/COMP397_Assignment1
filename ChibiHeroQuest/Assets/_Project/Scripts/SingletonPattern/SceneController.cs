/*
 * Source File: SceneController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-03-07
 * 
 * Program Description:
 * This program manages scene load.
 * 
 * Revision History:
 * - 2025-03-07: Initial version created.
 */

using UnityEngine.SceneManagement;

namespace Platformer397
{
    public class SceneController : Singleton<SceneController>
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
