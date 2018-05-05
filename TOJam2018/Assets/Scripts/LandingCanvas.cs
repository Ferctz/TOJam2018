using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Menu
{
    public class LandingCanvas : MonoBehaviour
    {
        public IntVariable playerCount;

        public GameEvent gameStartEvent;

        public void SetSinglePlayer()
        {
            playerCount.Value = 1;
            StartGame();
        }

        public void SetMultiplayer()
        {
            playerCount.Value = 2;
            StartGame();
        }

        private void StartGame()
        {
            gameStartEvent.Raise();
        }
    }
}