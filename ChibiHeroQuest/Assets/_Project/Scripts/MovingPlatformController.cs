/*
 * Source File: MovingPlatformController.cs
 * Author: YuHsuan Chen
 * Student Number: 301448975
 * Student Number:
 * Date Last Modified: 2025-02-22
 * 
 * Program Description:
 * This program manages the controller of player.
 * 
 * Revision History:
 * - 2025-02-22: Initial version created.
 */
using UnityEngine;

namespace Platformer397
{
    public class MovingPlatformController : MonoBehaviour
    {
        public Transform pointA, pointB; // 平台移動的起點與終點
        public float speed = 2f;
        private Vector3 target;

        void Start()
        {
            target = pointB.position;
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                target = (target == pointA.position) ? pointB.position : pointA.position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = this.transform; // 設置 Player 為平台的子物件
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = null; // 玩家離開平台時解除父子關係
            }
        }
    }
}
