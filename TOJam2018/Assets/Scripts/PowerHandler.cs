using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Class that ticks down a player power amount. Invokes a player death event
    /// and this handles any power additions.
    /// </summary>
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

        /// <summary>
        /// Ticks down player power. If power below 0, player death event invoked.
        /// </summary>
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

        /// <summary>
        /// Adds powerToAdd amount to player current power, ensuring it is clamped within player max power.
        /// </summary>
        /// <param name="powerToAdd"></param>
        public void AddPower(float powerToAdd)
        {
            playerPower.Value += powerToAdd;
            playerPower.Value = Mathf.Clamp(playerPower.Value, 0f, playerMaxPower.Value);
        }
    }
}