/*
 * Source File: EnemyNavigation.cs
 * Author: Class sample
 * Student Number:
 * Date Last Modified: 2025-02-01
 * 
 * Program Description:
 * This program manages the controller of player.
 * 
 * Revision History:
 * - 2025-02-01: Initial version created.
 */

using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class EnemyNavigation : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Transform player;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player").transform;
        }
        void Update()
        {
            agent.destination = player.position;
        }
    }
}
