using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;

namespace TOJAM2018.HUD
{
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

        public void Init(Camera playerCamera, FloatVariable currentPower, FloatVariable maxPower, BoolVariable invertY)
        {
            Canvas.worldCamera = playerCamera;
            sliderPowerTracker.Init(currentPower, maxPower);

            if (playerCount.Value == 1) // if solo
            {
                moveText.text += " / WASD";
                fireText.text += " / Space";
                boostText.text += " / Left Shift";
            }

            gameEndText.SetActive(false);
        }

        public void ShowGameEndScreen()
        {
            gameEndText.SetActive(true);
        }
    }
}