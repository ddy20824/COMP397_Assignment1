/*
 * Source File: TrapController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-03-01
 * 
 * Program Description:
 * This program manages trap.
 * 
 * Revision History:
 * - 2025-03-01: Initial version created.
 */
using UnityEngine;

namespace Platformer397
{
    public class TrapController : MonoBehaviour
    {
        [SerializeField] private LayerMask isPlayer;
        void OnCollisionEnter(Collision collision)
        {
            if (isPlayer == (isPlayer | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
        }
    }
}
