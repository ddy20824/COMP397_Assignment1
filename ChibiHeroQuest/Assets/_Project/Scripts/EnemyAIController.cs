using System;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class EnemyAIController : MonoBehaviour
    {
        public NavMeshAgent agent;
        public GameObject player;
        public LayerMask whatIsGround, whatIsPlayer;

        //Patroling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

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
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

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


                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.transform.position);
        }

        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanaceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanaceToWalkPoint.magnitude < 2f)
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                walkPointSet = true;
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
