using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;

namespace TOJAM2018.HUD
{
    public class SliderPowerTracker : MonoBehaviour
    {
        public Slider powerSlider;
        public FloatVariable maxPower;
        public FloatVariable currentPower;

        public void Init(FloatVariable currentPower, FloatVariable maxPower)
        {
            this.currentPower = currentPower;
            this.maxPower = maxPower;

            powerSlider.value = maxPower.Value;
            powerSlider.maxValue = maxPower.Value;
        }

        private void Update()
        {
            if (powerSlider == null || currentPower == null || maxPower == null)
            {
                return;
            }

            powerSlider.value = currentPower.Value;
        }
    }
}