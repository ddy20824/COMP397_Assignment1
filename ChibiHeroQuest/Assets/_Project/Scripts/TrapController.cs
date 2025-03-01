using UnityEngine;

namespace Platformer397
{
    public class TrapController : MonoBehaviour
    {
        [SerializeField] private LayerMask isPlayer;
        void OnCollisionEnter(Collision collision)
        {
            if (isPlayer == (isPlayer | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
        }
    }
}
