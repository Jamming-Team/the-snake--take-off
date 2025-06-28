using UnityEngine;
using XTools;

namespace Snake.MainMenu {
    public class MM_SceneController : MonoBehaviour {
        [SerializeField] StateMachine _stateMachine;

        void Start() {
            _stateMachine.Init(this, true);
        }
    }
}