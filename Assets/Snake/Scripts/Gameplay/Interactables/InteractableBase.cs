using System;
using UnityEngine;
using XTools;

namespace Snake {
    public abstract class InteractableBase : MonoBehaviour, IInteractable {
        [SerializeField] protected InteractableData _data;
        
        protected PlayerMediator _mediator;
        
        void Start() {
            ServiceLocator.For(this).Get(out _mediator);
        }

        public abstract void Interact();

        public InteractableData GetInteractableData() {
            return _data;
        }
    }
}