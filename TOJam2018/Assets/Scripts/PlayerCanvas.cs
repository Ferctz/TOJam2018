using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;

namespace TOJAM2018.HUD
{
    /// <summary>
    /// Handles initializing a player canvas based on power values and player count.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class PlayerCanvas : MonoBehaviour
    {
        private Canvas canvas;
        public Canvas Canvas { get { return canvas ?? (canvas = GetComponent<Canvas>()); } }

        public SliderPowerTracker sliderPowerTracker;

        public GameObject gameEndText;

        public IntVariable playerCount;

        public Text moveText;
        public Text fireText;
        public Text boostText;

        /// <summary>
        /// Method to initialize components in this canvas.
        /// </summary>
        /// <param name="playerCamera"> Render camera. </param>
        /// <param name="currentPower"> Player current power. </param>
        /// <param name="maxPower"> Player max power. </param>
        public void Init(Camera playerCamera, FloatVariable currentPower, FloatVariable maxPower)
        {
            if (!sliderPowerTracker || !gameEndText || !playerCount ||
                !moveText || !fireText || !boostText)
            {
                Debug.Log("PlayerCanvas variables not set.", this);
                return;
            }

            Canvas.worldCamera = playerCamera;

            // init power slider based on current/max power values
            sliderPowerTracker.Init(currentPower, maxPower);

            if (playerCount.Value == 1) // if solo
            {
                moveText.text += " / WASD";
                fireText.text += " / Space";
                boostText.text += " / Left Shift";
            }

            gameEndText.SetActive(false);
        }

        /// <summary>
        /// Sets active a text canvas component.
        /// </summary>
        public void ShowGameEndScreen()
        {
            if (gameEndText)
            {
                gameEndText.SetActive(true);
            }
        }
    }
}