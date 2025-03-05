/*
 * Source File: PlayerController.cs
 * Author: Class sample, Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the controller of player.
 * 
 * Revision History:
 * - 2025-02-01: Initial version created.
 * - 2025-02-21: Add groundCheck.
 * - 2025-02-22: Add cloud.
 * - 2025-02-23: Add death and reset, sound.
 */

using UnityEngine;

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
        [SerializeField] private int fallHeight = -10;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip onCloudSound;
        [SerializeField] private int health = 5;
        private Animator anim;
        private bool isTouchingGround;
        private float distToGround;
        private bool isAttacking;
        private bool isDamaging;
        private bool isDrawn;
        private int bouncyMag = 1;
        private Vector3 initLocation = new Vector3(-3f, 8f, 20f);
        private Quaternion initQuaternion = new Quaternion(0, 180, 0, 0);

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
            // input.LoadBinding(); // 載入上次的綁定
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
            FallCheck();
            GroundCheck();
            UpdateMovement();
        }

        private void FallCheck()
        {
            if (transform.position.y < fallHeight)
            {
                ReduceHealth();
                Reset();
            }
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
            if (Time.timeScale == 1)
            {
                if (isAttacking)
                    audioSource.PlayOneShot(attackSound);
                isAttacking = !isAttacking;
                anim.SetBool("IsAttacking", isAttacking);
            }
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
                audioSource.PlayOneShot(onCloudSound);
                bouncyMag = 2;
            }
            else
            {
                bouncyMag = 1;
            }
            if (collision.gameObject.tag == "Enemy")
            {
                TakeDamage();
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (isAttacking && other.gameObject.tag == "Enemy")
            {
                transform.LookAt(other.transform);
                var enemyController = other.gameObject.GetComponent<EnemyController>();
                enemyController.TakeDamage();
            }
        }
        public void Drawn()
        {
            if (!isDrawn)
            {
                isDrawn = true;
                ReduceHealth();
                anim.SetBool("IsDead", true);
                StartCoroutine(Helper.Delay(Reset, 1f));
                StartCoroutine(Helper.Delay(() => { isDrawn = false; }, 0.5f));
            }
        }

        public void Reset()
        {
            anim.SetBool("IsDead", false);
            transform.SetPositionAndRotation(initLocation, initQuaternion);
        }

        public void TakeDamage()
        {
            if (!isDamaging)
            {
                ReduceHealth();
                isDamaging = true;
                anim.SetTrigger("IsHurt");
                StartCoroutine(Helper.Delay(() => { isDamaging = false; }, 0.5f));
            }
        }

        void ReduceHealth()
        {
            health -= 1;
            EventManager.instance.TriggerUpdateHealth(health);
            if (health <= 0)
            {
                health = 0;
                anim.SetBool("IsDead", true);
                StartCoroutine(Helper.Delay(EventManager.instance.TriggerShowGameOver, 1f));
            }
        }
    }
}
