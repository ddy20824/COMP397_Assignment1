/*
 * Source File: ForestGroundFallingController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the Ground falling.
 * 
 * Revision History:
 * - 2025-02-23: When CollisionEnter add rigidbody.
 */

using UnityEngine;

namespace Platformer397
{
    public class ForestGroundFallingController : MonoBehaviour
    {
        public LayerMask isPlayer;

        void OnCollisionEnter(Collision collision)
        {
            if (isPlayer == (isPlayer | (1 << collision.gameObject.layer)))
            {
                StartCoroutine(Helper.Delay(AddRigidbody, 0.5f));
            }
        }

        private void AddRigidbody()
        {
            if (gameObject.GetComponent<Rigidbody>() == null)
                gameObject.AddComponent<Rigidbody>();
        }
    }
}
