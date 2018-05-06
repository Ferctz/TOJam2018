using UnityEngine;
using ScriptableObjects;
using Random = System.Random;
using TOJAM2018.HUD;

namespace TOJAM2018.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public IntVariable playerCount;

        public GameObject player1;
        public GameObject player2;

        public Camera playerCamera;

        public PlayerCanvas player1HUDCanvas;
        public PlayerCanvas player2HUDCanvas;

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

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void StartGame()
        {
            SceneManagement.SceneManager.LoadSceneAsync("Game", UnityEngine.SceneManagement.LoadSceneMode.Single, OnGameSceneLoaded);
        }

        private void OnGameSceneLoaded()
        {
            GameObject player1Clone = GameObject.Instantiate(player1);
            Camera player1Camera = Instantiate(playerCamera);
            FollowPlayer player1CameraFollow = player1Camera.GetComponent<FollowPlayer>();
            player1CameraFollow.FollowTransform = player1Clone.transform;

            PlayerCanvas player1Canvas = Instantiate(player1HUDCanvas);
            player1Canvas.Canvas.worldCamera = player1Camera;

            player1Clone.transform.position = new Vector3(Mathf.Lerp(gameTerrainData.bounds.min.x, gameTerrainData.bounds.max.x, (float)rand.NextDouble()),
                                                            0f,
                                                            Mathf.Lerp(gameTerrainData.bounds.min.z, gameTerrainData.bounds.max.z, (float)rand.NextDouble()));
            ray.origin = new Vector3(player1Clone.transform.position.x, 1000f, player1Clone.transform.position.z);
            ray.direction = Vector3.down;
            if (Physics.Raycast(ray, out hit, 1001f, terrainLayer))
            {
                player1Clone.transform.position = new Vector3(player1Clone.transform.position.x, hit.point.y + 50f, player1Clone.transform.position.z);
            }

            if (playerCount.Value > 1)
            {
                player1Camera.rect = new Rect(new Vector2(0f, 0.5f), new Vector2(1f, 0.5f));

                GameObject player2Clone = GameObject.Instantiate(player2);
                Camera player2Camera = Instantiate(playerCamera);
                FollowPlayer player2CameraFollow = player2Camera.GetComponent<FollowPlayer>();
                player2CameraFollow.FollowTransform = player2Clone.transform;

                PlayerCanvas player2Canvas = Instantiate(player2HUDCanvas);
                player2Canvas.Canvas.worldCamera = player2Camera;

                player2Camera.rect = new Rect(Vector2.zero, new Vector2(1f, 0.5f));

                player2Clone.transform.position = player1Clone.transform.position + new Vector3(Mathf.Lerp(-30f, 30f, (float)rand.NextDouble()),
                                                            0f,
                                                            Mathf.Lerp(-30f, 30f, (float)rand.NextDouble()));
            }
        }
    }
}