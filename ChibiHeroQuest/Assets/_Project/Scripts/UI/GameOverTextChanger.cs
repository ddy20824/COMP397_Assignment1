using TMPro;
using UnityEngine;

namespace Platformer397
{
    public class GameOverTextChanger : MonoBehaviour
    {
        public TextMeshProUGUI WinLoseSubTitle;
        public TextMeshProUGUI CollectableCountText;
        public TextMeshProUGUI RescueCountText;

        void Start()
        {
            // Check if both texts indicate that the player has collected the required items
            if (CollectableCountText.text == "3" && RescueCountText.text == "3")
            {
                    WinLoseSubTitle.text = "You win!";
            }
        }
    }
}
