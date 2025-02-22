using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class SlimeController : MonoBehaviour
    {
        private Animator anim;
        private NavMeshAgent agent;
        public GameObject player;
        public LayerMask whatIsGround, whatIsPlayer;

        //Patroling
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private float distanceThreshold = 1.0f;
        private Vector3 destination;

        //Attacking
        public float timeBetweenAttacks;
        bool alreadyAttacked;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        public int health;

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
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            transform.LookAt(player.transform);

            if (!alreadyAttacked)
            {
                //Attack code here

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

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Invoke(nameof(DestroyEnemy), 2f);
            }
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}
