using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Rajendra Abhinaya, 2023

namespace Platformer397
{
    public class DestructibleObject : MonoBehaviour
    {
        enum DebrisAmount
        {
            Low,
            Medium,
            High,
            Random
        }

        enum DespawnType
        {
            None,
            Timed,
            DistanceFromPlayer
        }

        [Header("Debris")]
        [SerializeField, Tooltip("List of debris prefabs that will be spawned when the object is destroyed. Editing the list is not recommended")]
        private GameObject debrisPrefab;

        [Header("Despawning")]

        [SerializeField, Range(0, 100), Tooltip("Percentage of debris objects that will be despawned")]
        private int despawnPercentage;

        [Header("Audio")]
        [SerializeField, Tooltip("List of audio clips that will be played when the object breaks. Audio clips are selected randomly from the list")]
        private List<AudioClip> audioClips = new List<AudioClip>();

        [SerializeField, Tooltip("Volume of the audio clip when played"), Range(0f, 1f)]
        private float volume;

        [SerializeField, Tooltip("Amount of variation in the volume of each audio clip played"), Range(0f, 0.2f)]
        private float volumeVariation;

        [SerializeField, Tooltip("Amount of variation in the pitch volume of each audio clip played"), Range(0f, 0.5f)]
        private float pitchVariation;

        private GameObject debris;
        private new Rigidbody rigidbody;

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

            //Sends variable values to the debris
            debris.GetComponent<Despawn>().SetVariables(despawnPercentage, audioClips[Random.Range(0, audioClips.Count)], volume, volumeVariation, pitchVariation);

            //Destroys the game object
            Destroy(gameObject);
        }

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            debris.SetActive(false);
        }
    }
}