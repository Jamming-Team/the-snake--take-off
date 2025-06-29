using UnityEngine;

namespace Snake {
    public class InteractableGeneric : InteractableBase {
        [SerializeField] string _requiredItemName;
        [SerializeField] bool _destroyOnInteract;
        [SerializeField] ActivatableBase _activatable;
        
        public override void Interact() {

            if (_requiredItemName != "") {
                if (!_mediator.playerInventory.TryUse(_requiredItemName)) {
                    return;
                }
            }
            
            _activatable.Activate();

            if (_destroyOnInteract) {
                Destroy(gameObject);
            }

            
            
        }
    }
}