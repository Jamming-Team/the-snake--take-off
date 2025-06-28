using UnityEngine;
using XTools;

namespace Snake {
    public class GP_SceneController : MonoBehaviour {
        [SerializeField] StateMachine _stateMachine;

        void Start() {
            _stateMachine.Init(this, true);
        }
    }
}