using UnityEngine;

namespace Platformer397
{
    public class LimitMiniMapCamera : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        void LateUpdate()
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 25, player.transform.position.z);
        }
    }
}
