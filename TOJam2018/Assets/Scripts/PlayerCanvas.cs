﻿using UnityEngine;
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

        public void Init(Camera playerCamera, FloatVariable currentPower, FloatVariable maxPower)
        {
            Canvas.worldCamera = playerCamera;
            sliderPowerTracker.Init(currentPower, maxPower);

            gameEndText.SetActive(false);
        }

        public void ShowGameEndScreen()
        {
            gameEndText.SetActive(true);
        }
    }
}