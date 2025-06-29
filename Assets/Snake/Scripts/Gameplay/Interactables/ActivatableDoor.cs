using UnityEngine;

namespace Snake {
    public class ActivatableDoor : ActivatableBase {
        [SerializeField] Collider _collider;
        
        [SerializeField] Animator _animator;

        public override void Activate() {
            _collider.enabled = !_collider.enabled;
            _animator.SetTrigger("Slide");
        }
    }
}