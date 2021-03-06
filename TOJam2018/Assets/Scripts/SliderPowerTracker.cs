﻿using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;

namespace TOJAM2018.HUD
{
    /// <summary>
    /// Class that tracks a power value. It is initialized based on a max value.
    /// </summary>
    public class SliderPowerTracker : MonoBehaviour
    {
        public Slider powerSlider;
        public FloatVariable maxPower;
        public FloatVariable currentPower;

        public void Init(FloatVariable playerCurrentPower, FloatVariable playerMaxPower)
        {
            if (!powerSlider)
            {
                return;
            }

            currentPower = playerCurrentPower;
            maxPower = playerMaxPower;

            powerSlider.value = maxPower.Value;
            powerSlider.maxValue = maxPower.Value;
        }

        private void Update()
        {
            if (powerSlider == null || currentPower == null)
            {
                return;
            }

            powerSlider.value = currentPower.Value;
        }
    }
}