using UnityEngine;

namespace Snake {
    public class ActivatableDoor : ActivatableBase {
        [SerializeField] Collider _collider;

        public override void Activate() {
            _collider.enabled = !_collider.enabled;
        }
    }
}