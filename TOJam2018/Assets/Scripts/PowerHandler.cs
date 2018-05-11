using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public class PowerHandler : MonoBehaviour
    {
        public BoolVariable isGameRunning;

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
            if (!isGameRunning.Value)
            {
                return;
            }

            playerPower.Value -= Time.deltaTime * powerCost.Value;

            if (playerPower.Value <= 0f)
            {
                if (playerDeathEvent != null)
                {
                    playerDeathEvent.Raise();
                }
            }
        }

        public void AddPower(float powerToAdd)
        {
            playerPower.Value += powerToAdd;
            playerPower.Value = Mathf.Clamp(playerPower.Value, 0f, playerMaxPower.Value);
        }
    }
}