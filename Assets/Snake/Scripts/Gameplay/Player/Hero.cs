using System;
using UnityEngine;

namespace Snake {
    [RequireComponent(typeof(PlayerController))]
    public class Hero : MonoBehaviour {

        [SerializeField] InputReader _inputReader;
        
        PlayerInventory _playerInventory;
        PlayerController _playerController;
        InteractComponent _interactComponent;
        PlayerMediator _playerMediator;

        void Awake() {
            _inputReader.EnablePlayerActions();
            
            _playerController = GetComponent<PlayerController>();
            _playerController.Init(_inputReader);
            
            _playerInventory = new PlayerInventory();
            
            _interactComponent = GetComponent<InteractComponent>();
            _interactComponent.Init(_inputReader, _playerMediator);

            _playerMediator = new PlayerMediator();
            _playerMediator.Init(_interactComponent, _playerInventory);
        }
    }
}