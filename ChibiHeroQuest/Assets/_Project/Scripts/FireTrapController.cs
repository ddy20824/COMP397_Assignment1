/*
 * Source File: FireTrapController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-03-01
 * 
 * Program Description:
 * This program manages fire trap.
 * 
 * Revision History:
 * - 2025-03-01: Initial version created.
 */
using UnityEngine;

namespace Platformer397
{
    public class FireTrapController : MonoBehaviour
    {
        [SerializeField] private GameObject fire;
        private bool fireOpening = true;

        void Start()
        {
            InvokeRepeating("Fire", 1.5f, 1.5f);
        }
        void Fire()
        {
            fireOpening = !fireOpening;
            fire.SetActive(fireOpening);
        }
    }
}
