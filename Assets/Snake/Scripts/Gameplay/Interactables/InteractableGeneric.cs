using System.Collections.Generic;
using UnityEngine;

namespace Snake {
    public class InteractableGeneric : InteractableBase {
        [SerializeField] string _requiredItemName;
        [SerializeField] bool _destroyOnInteract;
        [SerializeField] List<ActivatableBase> _activatables;
        
        public override void Interact() {

            if (_requiredItemName != "") {
                if (!_mediator.playerInventory.TryUse(_requiredItemName)) {
                    return;
                }
            }

            foreach (var activatable in _activatables) { 
                activatable.Activate();
            }

            if (_destroyOnInteract) {
                Destroy(gameObject);
            }

            
            
        }
    }
}