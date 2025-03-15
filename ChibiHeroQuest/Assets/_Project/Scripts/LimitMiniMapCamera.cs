/*
 * Source File: LimitMiniMapCamera.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-03-01
 * 
 * Program Description:
 * This program manages minimap.
 * 
 * Revision History:
 * - 2025-03-01: Initial version created.
 */
using UnityEngine;

namespace Platformer397
{
    public class LimitMiniMapCamera : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        void LateUpdate()
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 25, player.transform.position.z);
        }
    }
}
