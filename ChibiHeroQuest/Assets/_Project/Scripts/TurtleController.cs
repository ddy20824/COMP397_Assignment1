/*
 * Source File: TurtleController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the turtle enemy.
 * 
 * Revision History:
 * - 2025-02-22: Manage turtleEnemy action.
 * - 2025-02-23: Move same function to EnemyController.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class TurtleController : EnemyController
    {
        [SerializeField] private LayerMask whatIsPlayer;
        private int index = 0;
        private bool playerInAttackRange;

        void Update()
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, 1, whatIsPlayer);

            if (!playerInAttackRange) Patroling();
            if (playerInAttackRange) AttackPlayer();

        }

        private void Patroling()
        {
            if (Vector3.Distance(destination, transform.position) < distanceThreshold)
            {
                index = (index + 1) % waypoints.Count;
                destination = waypoints[index].position;
                agent.destination = destination;
            }
        }
    }
}
