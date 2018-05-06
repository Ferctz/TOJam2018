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

        public Toggle invertYToggle;
        public BoolVariable invertY;

        private void Awake()
        {
            invertYToggle.isOn = invertY.Value;
        }

        public void ToggleInvertY()
        {
            invertYToggle.isOn = !invertYToggle.isOn;
        }
    }
}