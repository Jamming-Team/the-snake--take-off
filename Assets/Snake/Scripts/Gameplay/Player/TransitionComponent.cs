using System;
using UnityEngine;
using XTools;

namespace Snake {
    public class TransitionComponent : MonoBehaviour {
        public Action OnTransition = delegate { };
        
        InputReader _input;

        public void Init(InputReader input) {
            _input = input;
            _input.Transit += InputOnTransit;
        }

        void OnDestroy() {
            _input.Transit -= InputOnTransit;
        }

        void InputOnTransit() {
            OnTransition.Invoke();
        }
    }
}