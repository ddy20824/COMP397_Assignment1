using UnityEngine;

namespace Platformer397
{
    public class ForestGroundFallingController : MonoBehaviour
    {
        public LayerMask isPlayer;

        void OnCollisionEnter(Collision collision)
        {
            if (isPlayer == (isPlayer | (1 << collision.gameObject.layer)))
            {
                StartCoroutine(Helper.Delay(AddRigidbody, 0.5f));
            }
        }

        private void AddRigidbody()
        {
            if (gameObject.GetComponent<Rigidbody>() == null)
                gameObject.AddComponent<Rigidbody>();
        }
    }
}
