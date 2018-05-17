using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Menu
{
    /// <summary>
    /// Method that handles input from landing scene. It sets player count
    /// and invokes game start event.
    /// </summary>
    public class LandingCanvas : MonoBehaviour
    {
        public IntVariable playerCount;

        public GameEvent gameStartEvent;

        public void SetSinglePlayer()
        {
            if (!playerCount)
            {
                return;
            }
            playerCount.Value = 1;
            StartGame();
        }

        public void SetMultiplayer()
        {
            if (!playerCount)
            {
                return;
            }
            playerCount.Value = 2;
            StartGame();
        }

        private void StartGame()
        {
            if (gameStartEvent)
            {
                gameStartEvent.Raise();
            }
        }
    }
}