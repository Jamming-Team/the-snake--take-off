using UnityEngine;

namespace Snake {
    public class InteractablePickUp : InteractableBase {
        [SerializeField] InventoryItem _item;
        [SerializeField] GameObject _additionalKey;
         
        public override void Interact() {
            _mediator.playerInventory.Add(_item);
            // mediator.playerInventory.Add(_data.item);
            Destroy(gameObject);

            if (_additionalKey) {
                Destroy(_additionalKey);
            }
        }


    }
}