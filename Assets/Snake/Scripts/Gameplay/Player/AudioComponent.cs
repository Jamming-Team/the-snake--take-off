using System;
using System.Collections;
using UnityEngine;
using XTools;

namespace Snake {
    public class AudioComponent : MonoBehaviour {

        [SerializeField] SoundData _lSteps;
        [SerializeField] SoundData _rSteps;
        [SerializeField] float _stepsInterval = 0.5f;
        
        [SerializeField] SoundData _jumpData;
        [SerializeField] SoundData _landData;
        [SerializeField] Transform _legsTr;

        PlayerController _playerController;
        AudioManager  _audioManager;

        public void Init(PlayerController playerController) {
            _playerController = playerController;
            
            _playerController.OnJump += PlayerControllerOnOnJump;
            _playerController.OnLand += PlayerControllerOnOnLand;
        }

        void OnDestroy() {
            _playerController.OnJump -= PlayerControllerOnOnJump;
            _playerController.OnLand -= PlayerControllerOnOnLand;
        }


        void Start() {
            ServiceLocator.For(this).Get(out _audioManager);
            StartCoroutine(StepLoop());
        }

        void PlayerControllerOnOnJump(Vector3 obj) {
            _audioManager.PlaySound(_jumpData, _legsTr);
            
            _audioManager.PlaySound(_lSteps, _legsTr);
            _audioManager.PlaySound(_rSteps, _legsTr);
            
        }
        
        void PlayerControllerOnOnLand(Vector3 obj) {
            _audioManager.PlaySound(_landData, _legsTr);
        }
        
        IEnumerator StepLoop()
        {
            while (true)
            {
                if (_playerController.IsMoving)
                    _audioManager.PlaySound(_lSteps, _legsTr);
                yield return new WaitForSeconds(_stepsInterval);


                if (_playerController.IsMoving)
                    _audioManager.PlaySound(_rSteps, _legsTr);
                yield return new WaitForSeconds(_stepsInterval);
            }

        }

    }
}