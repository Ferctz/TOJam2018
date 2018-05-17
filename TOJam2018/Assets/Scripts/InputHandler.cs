using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.InputHandling
{
    /// <summary>
    /// Only this class is designed to poll for key/mouse/controller input.
    /// Stores values in scriptable object containers.
    /// Mapping with Windows Xbox mapping in mind.
    /// Setup: inside Unity, per player create a horizontal and vertical axis.
    /// Make sure type is Joystick Axis, Axis is per axis and Joy Num the joystick number.
    /// </summary>
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

        /// <summary>
        /// Polling for player input for solo mode with keyboard/controller or 
        /// multiplayer mode with two players using controllers.
        /// </summary>
        private void Update()
        {
            if (!escEvent || !playerCount || !gameStartEvent ||
                !player1Horizontal || !player1Vertical || !player1Fire1Event ||
                !player1Boost || !player1InvertYEvent ||
                !player2Horizontal || !player2Vertical || !player2Fire1Event ||
                !player2Boost || !player2InvertYEvent)
            {
                Debug.Log("InputHandler variables not set.", this);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape)) // escape button press
            {
                escEvent.Raise();
            }

            if (Input.GetKeyDown("joystick button 7")) // start button press
            {
                playerCount.Value = 1;
                gameStartEvent.Raise();
            }

            if (Input.GetKeyDown("joystick button 6")) // select button press
            {
                playerCount.Value = 2;
                gameStartEvent.Raise();
            }

            #region PlayerOneInput

            if (playerCount.Value == 1) // solo mode supports keyboard input
            {
                // horizontal/vertical axis input
                player1Horizontal.Value = Input.GetAxis("Horizontal");
                player1Vertical.Value = Input.GetAxis("Vertical");

                if (Input.GetKeyDown(KeyCode.Space) || // space key press
                    Input.GetKeyDown("joystick 1 button 0")) // a button press
                {
                    player1Fire1Event.Raise();
                }

                if (Input.GetKeyDown(KeyCode.Y) || // y key press
                    Input.GetKeyDown("joystick 1 button 3")) // y button press
                {
                    player1InvertYEvent.Raise();
                }

                player1Boost.Value = Input.GetKey(KeyCode.LeftShift) || // left shift press
                                        Input.GetKey("joystick 1 button 5"); // right bumper press
            }
            else // multiplayer mode only accepts xbox controller input
            {
                // player 2 horizontal/vertical axis input
                player1Horizontal.Value = Input.GetAxis("Horizontal1");
                player1Vertical.Value = Input.GetAxis("Vertical1");

                if (Input.GetKeyDown("joystick 1 button 0")) // player 1 a button press
                {
                    player1Fire1Event.Raise();
                }

                if (Input.GetKeyDown("joystick 1 button 3")) // player 1 y button press
                {
                    player1InvertYEvent.Raise();
                }

                player1Boost.Value = Input.GetKey("joystick 1 button 5"); // player 1 right bumper press
            }       

            #endregion

            #region PlayerTwoInput

            // player 2 horizontal/vertical axis input
            player2Horizontal.Value = Input.GetAxis("Horizontal2");
            player2Vertical.Value = Input.GetAxis("Vertical2");

            if (Input.GetKeyDown("joystick 2 button 0")) // player 2 a button press
            {
                player2Fire1Event.Raise();
            }

            if (Input.GetKeyDown("joystick 2 button 3")) // player 2 y button press
            {
                player2InvertYEvent.Raise();
            }

            player2Boost.Value = Input.GetKey("joystick 2 button 5"); // player 2 right bumper press

            #endregion
        }
    }
}