using System;
using System.Collections;
using UnityEngine;

namespace XTools {
    public class SceneLoader : MonoBehaviour {
        internal const string CORE_SCENE_NAME = "Core";
        
        [SerializeField] SceneLoaderV _view;
        [SerializeField] string _initialSceneName = "MainMenu";
        internal string initialSceneName => _initialSceneName;
        readonly SceneLoaderM _model = new();

        bool _isLoading;

        void Awake() {
            ServiceLocator.Global.Register<SceneLoader>(this);
        }

        public void TryLoadScene(string sceneName, bool withAnims = false) {
            if (_isLoading) return;

            StartCoroutine(LoadSceneAsync(sceneName, withAnims));
        }

        IEnumerator LoadSceneAsync(string sceneName, bool withAnims = false) {
            _isLoading = true;

            if (withAnims) {
                _view.SetAnim(SceneLoaderV.LoadingAnims.In);
                yield return new WaitUntil(() => !_view.inProgress);
            }

            StartCoroutine(_model.LoadScene(sceneName));

            // yield return modelCor;
            yield return new WaitUntil(() => !_model.inProgress);
            yield return new WaitForSeconds(0.2f);

            if (withAnims) _view.SetAnim(SceneLoaderV.LoadingAnims.Out);

            _isLoading = false;
        }

    }
}