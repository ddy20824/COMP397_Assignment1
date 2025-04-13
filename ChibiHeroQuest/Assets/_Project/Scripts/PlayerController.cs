/*
 * Source File: PlayerController.cs
 * Author: Class sample, Chiayi Lin, YuHsuan Chen
 * Student Number: 301448962, 301448975
 * Date Last Modified: 2025-04-04
 * 
 * Program Description:
 * This program manages the controller of player.
 * 
 * Revision History:
 * - 2025-02-01: Initial version created.
 * - 2025-02-21: Add groundCheck.
 * - 2025-02-22: Add cloud.
 * - 2025-02-23: Add death and reset, sound.
 * - 2025-03-01: Add hurt.
 * - 2025-03-04: Add health and attack destructible crate.
 * - 2025-03-07: Add inventory.
 * - 2025-03-08: Add heal.
 * - 2025-03-09: Add save/load.
 * - 2025-03-12: Add victory animation.
 * - 2025-03-13: Fix fly high when uphill and add fall damage sound.
 * - 2025-03-14: Improve jump detect and fix event not unbind.
 * - 2025-04-04: Add Observer pattern
 */

using UnityEngine;

namespace Platformer397
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Subject, IDataPersistent
    {
        [SerializeField] private InputReader input;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 movement;
        [SerializeField] private float jumpSpeed = 2f;

        [SerializeField] private float moveSpeed = 200f;
        [SerializeField] private float rotationSpeed = 200f;

        [SerializeField] private Transform mainCam;
        [SerializeField] private LayerMask isCloud;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private int fallHeight = -10;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip onCloudSound;
        [SerializeField] private AudioClip fallSound;
        [SerializeField] private int health = 5;
        private Animator anim;
        private bool isTouchingGround;
        private float distToGround;
        private bool isAttacking;
        private bool isDamaging;
        private bool isDrawn;
        private float bouncyMag = 1;
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
            distToGround = transform.GetComponent<Collider>().bounds.extents.y + 0.1f;
            input.EnablePlayerActions();
        }

        private void OnEnable()
        {
            input.Move += GetMovement;
            input.Jump += HandleJump;
            input.Attack += HandleAttack;
            EventManager.instance.PlayerHeal += Heal;
        }

        private void OnDisable()
        {
            input.Move -= GetMovement;
            input.Jump -= HandleJump;
            input.Attack -= HandleAttack;
            EventManager.instance.PlayerHeal -= Heal;
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
                audioSource.PlayOneShot(fallSound);
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
            float speedMultiply = (isTouchingGround) ? 1f : 0.6f;
            var velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime * speedMultiply;
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
            if (GameState.Instance.GetQuestIndex() == 1)
            {
                NotifyObservers(ObserverType.Quest);
            }
            if (isTouchingGround)
            {
                anim.SetBool("IsJumping", true);
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSpeed * bouncyMag, rb.linearVelocity.z);
            }
        }
        private void GroundCheck()
        {
            float sphereRadius = 0.5f;
            Vector3 origin = transform.position + Vector3.up * 1f; // avoid origin is in collider

            if (Physics.SphereCast(origin, sphereRadius, Vector3.down, out RaycastHit hit, distToGround))
            {
                Debug.DrawRay(origin, Vector3.down * distToGround, Color.green, 1.0f);
                if (!isTouchingGround)
                {
                    isTouchingGround = true;
                    anim.SetBool("IsJumping", false);
                }
            }
            else
            {
                Debug.DrawRay(origin, Vector3.down * distToGround, Color.red, 1.0f);
                isTouchingGround = false;
            }
        }

        private void GetMovement(Vector2 move)
        {
            if (GameState.Instance.GetQuestIndex() == 0)
            {
                NotifyObservers(ObserverType.Quest);
            }
            movement.x = move.x;
            movement.z = move.y;
        }
        void OnCollisionEnter(Collision collision)
        {
            if (isCloud == (isCloud | (1 << collision.gameObject.layer)))
            {
                audioSource.PlayOneShot(onCloudSound);
                bouncyMag = 2.5f;
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
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Goal")
            {
                transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                anim.SetTrigger("Victory");
                GameState.Instance.SetIsWin(true);
                StartCoroutine(Helper.Delay(EventManager.instance.TriggerShowGameOver, 3f));
            }
        }
        void OnTriggerStay(Collider other)
        {
            if (isAttacking)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    transform.LookAt(other.transform);
                    var enemyController = other.gameObject.GetComponent<EnemyController>();
                    enemyController.TakeDamage();
                }
                if (other.gameObject.tag == "Destructible")
                {
                    var destructibleObject = other.gameObject.GetComponent<DestructibleObject>();
                    destructibleObject.Break();
                }
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
                GameState.Instance.SetIsWin(false);
                StartCoroutine(Helper.Delay(EventManager.instance.TriggerShowGameOver, 1f));
            }
        }

        void Heal()
        {
            if (health < 5)
            {
                health += 1;
                GameState.Instance.RemoveInventory(ItemData.HealPosion);
                EventManager.instance.TriggerUpdateHealth(health);
            }
        }

        public void LoadData(GameState data)
        {
            transform.position = data.GetPlayerPosition();
            health = data.GetPlayerHealth();
            EventManager.instance.TriggerUpdateHealth(health);
            Debug.Log("Load health");
        }

        public void SaveData()
        {
            GameState.Instance.SetPlayerPosition(transform.position);
            GameState.Instance.SetPlayerHealth(health);
        }
    }
}
