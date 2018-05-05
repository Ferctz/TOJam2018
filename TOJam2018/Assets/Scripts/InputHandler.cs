using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public GameEvent escEvent;

        [Header("Player 1 Input")]
        public FloatVariable player1Horizontal;
        public FloatVariable player1Vertical;
        public GameEvent player1Fire1Event;

        [Header("Player 2 Input")]
        public FloatVariable player2Horizontal;
        public FloatVariable player2Vertical;
        public GameEvent player2Fire1Event;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escEvent.Raise();
            }

            #region PlayerOneInput
            player1Horizontal.Value = Input.GetAxis("Horizontal");
            player1Vertical.Value = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) ||
                Input.GetButtonDown("Fire1"))
            {
                player1Fire1Event.Raise();
            }

            #endregion

            #region PlayerTwoInput

            player2Horizontal.Value = Input.GetAxis("Horizontal");
            player2Vertical.Value = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.RightShift) ||
                Input.GetButtonDown("Fire1"))
            {
                player2Fire1Event.Raise();
            }

            #endregion
        }
    }
}