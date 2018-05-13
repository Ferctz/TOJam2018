using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOJAM2018.SceneManagement
{
    public delegate void SceneLoadedEventHandler();

    /// <summary>
    /// Class that manages scene loading. Checks if scene name can be loaded. Invokes scene
    /// load event once asynchronous scene loading is successful.
    /// </summary>
    public class SceneManager : MonoBehaviour
    {
        private static event SceneLoadedEventHandler OnSceneLoaded;

        /// <summary>
        /// Method that checks is sceneName is included in build settings.
        /// </summary>
        /// <param name="sceneName"> Scene name string.</param>
        /// <returns> True if scene can be loaded, else false. </returns>
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
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
        }

        /// <summary>
        /// Public method for loading a scene, 
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
                OnSceneLoaded += (() => sceneLoadAction());
            }

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            return true;
        }

        /// <summary>
        /// Method returned when a scene is loaded. Invokes scene loaded event.
        /// </summary>
        /// <param name="scene"> Scene successfully loaded. </param>
        /// <param name="mode"> Load mode type. </param>
        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name + ". Mode: " + mode);

            if (OnSceneLoaded != null)
            {
                OnSceneLoaded();
            }
            OnSceneLoaded = null;
        }

        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SceneLoaded;
        }
    }
}