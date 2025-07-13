using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using XTools;
using static SnakeInputActions;

namespace Snake {
    public interface IInputReader
    {
        void EnablePlayerActions();
        void DisablePlayerActions();
    }
    
    [CreateAssetMenu(fileName = "SnakeInputReader", menuName = "Snake/SnakeInputReader", order = 0)]
    public class InputReader : ScriptableObject, IInputReader, IGameplayActions {
        
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction Interact = delegate { };
        public event UnityAction Transit = delegate { };
        
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
            // throw new NotImplementedException();
        }

        public void OnLook(InputAction.CallbackContext context) {
            // throw new NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    Jump.Invoke(true);
                    EventBus<JumpPressed>.Raise(new JumpPressed());
                    break;
                case InputActionPhase.Canceled:
                    Jump.Invoke(false);
                    break;
            }
        }

        public void OnInteract(InputAction.CallbackContext context) {
            if (context.started) {
                Interact.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context) {
            EventBus<UIButtonPressed>.Raise(new UIButtonPressed {
                buttonType = UIButtonPressed.UIButtons.Pause,
            });
        }

        public void OnTransit(InputAction.CallbackContext context) {
            Transit.Invoke();
        }
    }
    
    public struct JumpPressed : IEvent {}
}