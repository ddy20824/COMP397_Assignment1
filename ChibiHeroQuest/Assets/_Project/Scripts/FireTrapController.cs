using UnityEngine;

namespace Platformer397
{
    public class FireTrapController : MonoBehaviour
    {
        [SerializeField] private GameObject fire;
        private bool fireOpening = true;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InvokeRepeating("Fire", 1.5f, 1.5f);
        }
        void Fire()
        {
            fireOpening = !fireOpening;
            fire.SetActive(fireOpening);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
