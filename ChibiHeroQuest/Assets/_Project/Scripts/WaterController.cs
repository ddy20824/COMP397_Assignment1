using UnityEngine;

namespace Platformer397
{
    public class WaterController : MonoBehaviour
    {
        public LayerMask isPlayer;
        private PlayerController playerController;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (isPlayer == (isPlayer | (1 << other.gameObject.layer)))
            {
                StartCoroutine(Helper.Delay(playerController.Dead, 0.3f));
            }
        }
    }
}
