/*
 * Source File: DestructibleObject.cs
 * Author: Rajendra Abhinaya, YuHsuan Chen, Chiayi Lin
 * Student Number: 301448975, 301448962
 * Date Last Modified: 2025-04-11
 * 
 * Program Description:
 * This program manages destructible objects
 * Reference from the assets made by Rajendra Abhinaya, 2023
 * 
 * Revision History:
 * - 2025-03-07: Initial version created.
 * - 2025-03-08: Save status and change status when load.
 * - 2025-04-04: Add Observer pattern
 * - 2025-04-11: Add Quest complete notify
 */
using UnityEngine;

namespace Platformer397
{
    public class DestructibleObject : MonoBehaviour, IDataPersistent
    {
        [SerializeField] private GameObject debrisPrefab;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;
        private PlayerController player;
        private GameObject debris;
        private new Rigidbody rigidbody;
        private GameState gamestate;

        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            rigidbody = GetComponent<Rigidbody>();
            debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            debris.SetActive(false);
            gamestate = GameState.Instance;
        }

        public void Break()
        {
            float velocityMagnitude = rigidbody.linearVelocity.magnitude;

            //Activates the debris object and sets its position and rotation to match the object's
            debris.transform.position = transform.position;
            debris.transform.rotation = transform.rotation;
            debris.transform.localScale = transform.localScale;
            debris.SetActive(true);

            //Applies force to the debris based on the velocity of the object
            for (int i = 0; i < debris.transform.childCount; i++)
            {
                Rigidbody debrisRigidbody = debris.transform.GetChild(i).GetComponent<Rigidbody>();
                Vector3 randomise = new Vector3(Random.Range(0f, velocityMagnitude), Random.Range(0f, velocityMagnitude), Random.Range(0f, velocityMagnitude)) / 2;
                debrisRigidbody.linearVelocity = rigidbody.linearVelocity + randomise;
            }

            // Play the sound
            audioSource.PlayOneShot(audioClip);

            //Destroys the game object
            Destroy(gameObject);

            gamestate.SetRescueCount(gamestate.GetRescueCount() + 1);
            player.NotifyObservers(ObserverType.Achievement);
            if (GameState.Instance.GetQuestIndex() == 4)
            {
                player.GetComponent<PlayerController>().NotifyObservers(ObserverType.Quest);
            }
            gamestate.SetDestructibleObjectName(name);
        }

        public void LoadData(GameState data)
        {
            if (GameState.Instance.CheckDestructibleObjectNameExist(name))
            {
                Destroy(gameObject);
            }
        }

        public void SaveData()
        {
        }
    }
}