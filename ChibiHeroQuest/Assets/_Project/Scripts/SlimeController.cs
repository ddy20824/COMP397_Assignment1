/*
 * Source File: SlimeController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the slime enemy.
 * 
 * Revision History:
 * - 2025-02-22: Manage slimeEnemy action.
 * - 2025-02-23: Move same function to EnemyController.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class SlimeController : EnemyController
    {
        [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
        [SerializeField] private float sightRange, attackRange;
        private bool playerInSightRange, playerInAttackRange;

        // Update is called once per frame
        void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);
            base.AttackPlayer();
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.transform.position);
        }

        private void Patroling()
        {
            if (Vector3.Distance(destination, transform.position) < distanceThreshold)
            {
                int randomIndex = UnityEngine.Random.Range(0, waypoints.Count);
                destination = waypoints[randomIndex].position;
                agent.SetDestination(destination);
            }
        }
    }
}
