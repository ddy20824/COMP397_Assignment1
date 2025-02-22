using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class TurtleController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Animator anim;
        [SerializeField] private GameObject player;
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private float distanceThreshold = 1.0f;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private float timeBetweenAttacks;
        private int index = 0;
        private Vector3 destination;
        private bool alreadyAttacked;
        private bool playerInAttackRange;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            destination = waypoints[index].position;
        }

        private void Start()
        {
            agent.destination = destination;

        }
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


        private void AttackPlayer()
        {
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
    }
}
