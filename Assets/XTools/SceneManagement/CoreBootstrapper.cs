using UnityEngine;
using UnityEngine.SceneManagement;

namespace XTools {
    internal class CoreBootstrapper {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init() {
            Debug.Log("Core Bootstrapper Init");
            SceneManager.LoadSceneAsync(SceneLoader.CORE_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}