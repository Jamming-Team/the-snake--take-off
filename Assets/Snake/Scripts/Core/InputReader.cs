using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static SnakeInputActions;

namespace Snake {
    public interface IInputReader
    {
        void EnablePlayerActions();
        void DisablePlayerActions();
    }
    
    public class InputReader : ScriptableObject, IInputReader, IGameplayActions {
        
        public event UnityAction<bool> Jump = delegate { };
        
        SnakeInputActions _inputActions;
        
        public Vector2 direction => _inputActions.Gameplay.Move.ReadValue<Vector2>();
        public Vector2 lookDirection => _inputActions.Gameplay.Look.ReadValue<Vector2>();
        
        
        public void EnablePlayerActions() {
            if (_inputActions == null)
            {
                _inputActions = new SnakeInputActions();
                _inputActions.Gameplay.SetCallbacks(this);
            }
            _inputActions.Enable();
        }
        
        public void DisablePlayerActions() {
            _inputActions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context) {
            throw new NotImplementedException();
        }

        public void OnLook(InputAction.CallbackContext context) {
            throw new NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context) {
            throw new NotImplementedException();
        }
    }
}