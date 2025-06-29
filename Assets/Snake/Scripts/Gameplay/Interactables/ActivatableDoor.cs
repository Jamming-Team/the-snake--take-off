using System;
using UnityEngine;
using XTools;

namespace Snake {
    public class ActivatableDoor : ActivatableBase {
        [SerializeField] Collider _collider;
        [SerializeField] SoundData _openDoorSound;
        
        [SerializeField] Animator _animator;

        bool _shouldSlide;

        // void OnEnable() {
        //     if (_shouldSlide) {
        //         _animator.SetTrigger("Slide");
        //         _shouldSlide = false;
        //     }
        // }
        
        AudioManager _audioManager;

        void Start() {
            ServiceLocator.For(this).Get(out _audioManager);
        }

        void Update() {
            
            // Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"));
            
            if (_shouldSlide && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) {
                _shouldSlide = false;
                _animator.SetTrigger("Slide");
            }
        }

        public override void Activate() {
            
            _shouldSlide = true;
            _collider.enabled = !_collider.enabled;
            _animator.SetTrigger("Slide");
            _audioManager.PlaySound(_openDoorSound, transform);
        }
    }
}