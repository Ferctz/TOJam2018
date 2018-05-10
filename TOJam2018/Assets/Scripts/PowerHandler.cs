using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public class PowerHandler : MonoBehaviour
    {
        public FloatVariable powerCost;
        public FloatVariable playerPower;
        public FloatVariable playerMaxPower;

        public GameEvent playerDeathEvent;

        private void Awake()
        {
            playerPower.Value = playerMaxPower.Value;
        }

        private void Update()
        {
            playerPower.Value -= Time.deltaTime * powerCost.Value;

            if (playerPower.Value <= 0f)
            {
                if (playerDeathEvent != null)
                {
                    playerDeathEvent.Raise();
                }
            }
        }
    }
}