using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOJAM2018.SceneManagement
{
    public delegate void SceneLoadedCallback();

    public class SceneManager : MonoBehaviour
    {
        private static SceneLoadedCallback sceneLoadedEvent;

        public static bool IsSceneInBuildSettings(string sceneName)
        {
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                int lastSlash = scenePath.LastIndexOf("/");
                string nameOfScene = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

                if (nameOfScene == sceneName)
                {
                    return true;
                }
            }

            return false;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// Public method for loading a scene
        /// </summary>
        /// <param name="sceneName"> Scene name </param>
        /// <param name="loadSceneMode"> Scene loading mode </param>
        public static bool LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode, System.Action sceneLoadAction = null)
        {
            if (!IsSceneInBuildSettings(sceneName))
            {
                return false;
            }

            if (sceneLoadAction != null)
            {
                sceneLoadedEvent += (() => sceneLoadAction());
            }

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            return true;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name + ". Mode: " + mode);

            if (sceneLoadedEvent != null)
            {
                sceneLoadedEvent();
            }
            sceneLoadedEvent = null;
        }

        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}