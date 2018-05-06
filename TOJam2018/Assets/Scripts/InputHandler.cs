using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public GameEvent escEvent;

        public IntVariable playerCount;
        public GameEvent gameStartEvent;

        [Header("Player 1 Input")]
        public FloatVariable player1Horizontal;
        public FloatVariable player1Vertical;
        public GameEvent player1Fire1Event;
        public BoolVariable player1Boost;
        public GameEvent player1InvertYEvent;

        [Header("Player 2 Input")]
        public FloatVariable player2Horizontal;
        public FloatVariable player2Vertical;
        public GameEvent player2Fire1Event;
        public BoolVariable player2Boost;
        public GameEvent player2InvertYEvent;

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

            if (Input.GetKeyDown("joystick button 7")) // start button
            {
                playerCount.Value = 1;
                gameStartEvent.Raise();
            }

            if (Input.GetKeyDown("joystick button 6")) // select button
            {
                playerCount.Value = 2;
                gameStartEvent.Raise();
            }

            #region PlayerOneInput

            player1Horizontal.Value = Input.GetAxis("Horizontal1");
            player1Vertical.Value = Input.GetAxis("Vertical1");

            if (Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown("joystick 1 button 0"))
            {
                player1Fire1Event.Raise();
            }

            if (Input.GetKeyDown("joystick 1 button 3"))
            {
                player1InvertYEvent.Raise();
            }

            player1Boost.Value = Input.GetKey("joystick 1 button 5");

            #endregion

            #region PlayerTwoInput

            player2Horizontal.Value = Input.GetAxis("Horizontal2");
            player2Vertical.Value = Input.GetAxis("Vertical2");

            if (Input.GetKeyDown(KeyCode.RightShift) ||
                Input.GetKeyDown("joystick 2 button 0"))
            {
                player2Fire1Event.Raise();
            }

            if (Input.GetKeyDown("joystick 2 button 3"))
            {
                player2InvertYEvent.Raise();
            }

            player2Boost.Value = Input.GetKey("joystick 2 button 5");

            #endregion
        }
    }
}