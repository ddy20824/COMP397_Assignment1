/*
 * Source File: WaterController.cs
 * Author: Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the water.
 * 
 * Revision History:
 * - 2025-02-23: When TriggerEnter player drown.
 */

using UnityEngine;

namespace Platformer397
{
    public class WaterController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip sound;
        [SerializeField] private LayerMask isPlayer;
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
                audioSource.PlayOneShot(sound);
                StartCoroutine(Helper.Delay(playerController.Dead, 0.3f));
            }
        }
    }
}
