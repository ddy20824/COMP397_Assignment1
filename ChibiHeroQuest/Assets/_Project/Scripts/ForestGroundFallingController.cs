/*
 * Source File: ForestGroundFallingController.cs
 * Author: Chiayi Lin, YuHsuan Chen
 * Student Number: 301448962, 301448975
 * Date Last Modified: 2025-03-09
 * 
 * Program Description:
 * This program manages the Ground falling.
 * 
 * Revision History:
 * - 2025-02-23: When CollisionEnter add rigidbody.
 * - 2025-03-09: Add SaveData/LoadData.
 */
using UnityEngine;

namespace Platformer397
{
    public class ForestGroundFallingController : MonoBehaviour, IDataPersistent
    {
        public LayerMask isPlayer;

        void OnCollisionEnter(Collision collision)
        {
            if (isPlayer == (isPlayer | (1 << collision.gameObject.layer)))
            {
                GameState.Instance.SetFallingGroundName(name);
                StartCoroutine(Helper.Delay(AddRigidbody, 0.5f));
            }
        }

        private void AddRigidbody()
        {
            if (gameObject.GetComponent<Rigidbody>() == null)
            {
                gameObject.AddComponent<Rigidbody>();
                StartCoroutine(Helper.Delay(() => { gameObject.SetActive(false); }, 1.0f));
            }
        }

        public void LoadData(GameState data)
        {
            if (GameState.Instance.CheckFallingGroundNameExist(name))
            {
                gameObject.SetActive(false);
            }
        }

        public void SaveData()
        {
        }
    }
}
