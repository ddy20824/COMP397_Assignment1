/*
 * Source File: PlayerController.cs
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
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 movement;
        [SerializeField] private float jumpSpeed = 2f;

        [SerializeField] private float moveSpeed = 200f;
        [SerializeField] private float rotationSpeed = 200f;

        [SerializeField] private Transform mainCam;
        [SerializeField] private LayerMask isCloud;
        private Animator anim;
        private bool isTouchingGround;
        private float distToGround;
        private bool isAttacking;
        private int bouncyMag = 1;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            rb.freezeRotation = true;
            mainCam = Camera.main.transform;
            isAttacking = false;
        }

        private void Start()
        {
            distToGround = transform.GetComponent<Collider>().bounds.extents.y;
            input.EnablePlayerActions();
        }

        private void OnEnable()
        {
            input.Move += GetMovement;
            input.Jump += HandleJump;
            input.Attack += HandleAttack;
        }

        private void OnDisable()
        {
            input.Move -= GetMovement;
            input.Jump -= HandleJump;
            input.Attack += HandleAttack;
        }

        private void OnDestroy()
        {
        }

        private void FixedUpdate()
        {
            GroundCheck();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if (adjustedDirection.magnitude > 0f)
            {
                // Handle the rotation and movement
                HandleRotation(adjustedDirection);
                HandleMovement(adjustedDirection);
                anim.SetBool("IsWalking", true);
            }
            else
            {
                // not change the rotation or movement, but need to apply rigidbody Y movement for gravity
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
                anim.SetBool("IsWalking", false);
            }
        }

        private void HandleMovement(Vector3 adjustedDirection)
        {
            var velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }

        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void HandleAttack()
        {
            isAttacking = !isAttacking;
            anim.SetBool("IsAttacking", isAttacking);
        }

        private void HandleJump()
        {
            if (isTouchingGround)
            {
                anim.SetBool("IsJumping", true);
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSpeed * bouncyMag, rb.linearVelocity.z);
            }
        }
        private void GroundCheck()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround))
            {
                if (!isTouchingGround)
                {
                    isTouchingGround = true;
                    anim.SetBool("IsJumping", false);
                }
            }
            else
            {
                isTouchingGround = false;
            }
        }

        private void GetMovement(Vector2 move)
        {
            movement.x = move.x;
            movement.z = move.y;
        }
        void OnCollisionEnter(Collision collision)
        {
            if (isCloud == (isCloud | (1 << collision.gameObject.layer)))
            {
                bouncyMag = 2;
            }
            else
            {
                bouncyMag = 1;
            }
        }
    }
}
