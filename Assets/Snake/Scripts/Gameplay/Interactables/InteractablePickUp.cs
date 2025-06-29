using UnityEngine;

namespace Snake {
    public class InteractablePickUp : InteractableBase {
        [SerializeField] InventoryItem _item;
         
        public override void Interact() {
            _mediator.playerInventory.Add(_item);
            // mediator.playerInventory.Add(_data.item);
            Destroy(gameObject);
        }


    }
}