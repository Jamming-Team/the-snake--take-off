using System;
using UnityEngine;

namespace Snake {
    public class InteractComponent : MonoBehaviour {
        public Action<InteractableData> OnEnterInteract = delegate {};
        
        [SerializeField] LayerMask _interactableLayerMask;
        [SerializeField] Transform _cameraTransform;
        [SerializeField] float _castLength = 2f;
        
        RaycastHit _hitInfo;
        Collider _oldCollider;
        PlayerMediator _mediator;
        
        InputReader _inputReader;
        
        public void Init(InputReader input, PlayerMediator mediator) {
            input.Interact += OnInteract;
            _mediator = mediator;
            _inputReader = input;
        }

        void OnDestroy() {
            _inputReader.Interact -= OnInteract;
        }

        void OnInteract() {
            if (_oldCollider) {
                _oldCollider.GetComponent<IInteractable>().Interact();
            }
        }


        void FixedUpdate() {
            Cast();

            UpdateCurrentResult();
        }

        void Cast() {
            Physics.Raycast(transform.position, _cameraTransform.forward, out _hitInfo, _castLength, _interactableLayerMask);
        }

        void UpdateCurrentResult() {
            
            if (!_hitInfo.collider) {
                _oldCollider = null;
                OnEnterInteract.Invoke(null);
                return;
            }
            
            if (_hitInfo.collider && _hitInfo.collider != _oldCollider) {
                _oldCollider = _hitInfo.collider;
                OnEnterInteract.Invoke(_oldCollider.GetComponent<IInteractable>().GetInteractableData());
            }

        }
        
        
    }
}