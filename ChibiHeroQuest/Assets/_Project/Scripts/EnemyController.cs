/*
 * Source File: EnemyController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the enemy.
 * 
 * Revision History:
 * - 2025-02-23: Add enemy die while being attacked.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class EnemyController : MonoBehaviour, IDataPersistent
    {
        [SerializeField] protected List<Transform> waypoints = new List<Transform>();
        [SerializeField] protected GameObject player;
        [SerializeField] protected float timeBetweenAttacks;
        [SerializeField] protected float distanceThreshold = 1.0f;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip deadSound;
        protected Animator anim;
        protected NavMeshAgent agent;
        protected Vector3 destination;
        bool alreadyAttacked;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            destination = waypoints[0].position;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            agent.destination = destination;
        }

        // Update is called once per frame
        void Update()
        {

        }
        protected void AttackPlayer()
        {
            transform.LookAt(player.transform);

            if (!alreadyAttacked)
            {
                //Attack code here

                audioSource.PlayOneShot(attackSound);
                anim.SetBool("IsAttacking", true);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        private void ResetAttack()
        {
            alreadyAttacked = false;
            anim.SetBool("IsAttacking", false);
        }

        public void TakeDamage()
        {
            audioSource.PlayOneShot(deadSound);
            alreadyAttacked = true;
            anim.SetBool("IsDead", true);
            GameState.Instance.SetEnemyName(name);
            Invoke(nameof(DestroyEnemy), 1f);
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        public void LoadData(GameState data)
        {
            if (GameState.Instance.CheckEnemyNameExist(name))
            {
                Destroy(gameObject);
            }
        }

        public void SaveData()
        {
        }
    }
}
