using UnityEngine;
using ScriptableObjects;
using Random = System.Random;
using TOJAM2018.HUD;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Class that handles loading the game scene, spawns players and associated gameobjects.
    /// Requires a TerrainData asset. Uses its height to position players.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Player1")]
        public GameObject player1;
        public FloatVariable player1Power;

        [Header("Player2")]
        public GameObject player2;
        public FloatVariable player2Power;

        [Header("Global")]
        public BoolVariable isGameRunning;
        public IntVariable playerCount;
        public FloatVariable playerPowerCost;
        public FloatVariable playerMaxPower;
        public Camera playerCamera;
        public Camera playerUICamera;

        public PlayerCanvas playerHUDCanvas;

        public TerrainData gameTerrainData;

        private Random rand;
        private Ray ray = new Ray();
        private RaycastHit hit;
        public LayerMask terrainLayer;
        
        private void Awake()
        {
            rand = new Random((int)System.DateTime.Now.Ticks);
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Method that stops game if within Unity. If in standalone it quits application.
        /// </summary>
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        /// <summary>
        /// Method that loads game scene asynchronously.
        /// </summary>
        public void StartGame()
        {
            SceneManagement.SceneManager.LoadSceneAsync("Game", UnityEngine.SceneManagement.LoadSceneMode.Single, GameSceneLoaded);
        }

        /// <summary>
        /// Method that handles instantiation of players once the game scene loads. Inits things like
        /// player canvases, cameras, 
        /// </summary>
        private void GameSceneLoaded()
        {
            if (!player1 || !player1Power || !player2 || !player2Power ||
                !isGameRunning || !playerCount || !playerPowerCost || !playerMaxPower ||
                !playerCamera || !playerUICamera || !playerHUDCanvas ||
                !gameTerrainData
                )
            {
                Debug.Log("GameManager variables not set.", this);
                return;
            }

            // start game
            isGameRunning.Value = true;

            GameObject player1Clone = GameObject.Instantiate(player1);

            // spawn player 1 camera
            Camera player1Camera = Instantiate(playerCamera);
            FollowPlayer player1CameraFollow = player1Camera.GetComponent<FollowPlayer>();
            player1CameraFollow.FollowTransform = player1Clone.transform;
            player1CameraFollow.gameObject.name = "Player1Camera";

            Camera player1UICamera = Instantiate(playerUICamera);
            player1UICamera.gameObject.name = "Player1UICamera";

            // init player 1 canvas
            PlayerCanvas player1Canvas = Instantiate(playerHUDCanvas);
            player1Canvas.Init(player1UICamera, player1Power, playerMaxPower);
            player1Canvas.gameObject.name = "Player1Canvas";

            // position player 1 above the terrain
            player1Clone.transform.position = new Vector3(Mathf.Lerp(gameTerrainData.bounds.min.x, gameTerrainData.bounds.max.x, (float)rand.NextDouble()),
                                                            0f,
                                                            Mathf.Lerp(gameTerrainData.bounds.min.z, gameTerrainData.bounds.max.z, (float)rand.NextDouble()));

            // raycast down to make sure player one is always 50 units above the terrain
            ray.origin = new Vector3(player1Clone.transform.position.x, 1000f, player1Clone.transform.position.z);
            ray.direction = Vector3.down;
            if (Physics.Raycast(ray, out hit, 1001f, terrainLayer))
            {
                player1Clone.transform.position = new Vector3(player1Clone.transform.position.x, hit.point.y + 50f, player1Clone.transform.position.z);
            }

            if (playerCount.Value > 1)
            {
                // set player 1 rect to be half height
                Rect player1Rect = new Rect(new Vector2(0f, 0.5f), new Vector2(1f, 0.5f));
                player1Camera.rect = player1Rect;
                player1UICamera.rect = player1Rect;

                // spawn player 2
                GameObject player2Clone = GameObject.Instantiate(player2);

                // spawn player 2 camera
                Camera player2Camera = Instantiate(playerCamera);
                FollowPlayer player2CameraFollow = player2Camera.GetComponent<FollowPlayer>();
                player2CameraFollow.FollowTransform = player2Clone.transform;
                player1CameraFollow.gameObject.name = "Player1Camera";

                // spawn player2 UI camera
                Camera player2UICamera = Instantiate(playerUICamera);
                player2UICamera.gameObject.name = "Player2UICamera";

                // init player 2 canvas
                PlayerCanvas player2Canvas = Instantiate(playerHUDCanvas);
                player2Canvas.Init(player2UICamera, player2Power, playerMaxPower);
                player2Canvas.gameObject.name = "Player2Canvas";

                // set player 1 rect half height
                Rect player2Rect = new Rect(Vector2.zero, new Vector2(1f, 0.5f));
                player2Camera.rect = player2Rect;
                player2UICamera.rect = player2Rect;

                // position player 2 +-30m away in x & z from player 1
                player2Clone.transform.position = player1Clone.transform.position + new Vector3(Mathf.Lerp(-30f, 30f, (float)rand.NextDouble()),
                                                            0f,
                                                            Mathf.Lerp(-30f, 30f, (float)rand.NextDouble()));
            }
        }

        /// <summary>
        /// Ends the current game session.
        /// </summary>
        public void EndGame()
        {
            if (!isGameRunning)
            {
                return;
            }
            isGameRunning.Value = false;
        }
    }
}