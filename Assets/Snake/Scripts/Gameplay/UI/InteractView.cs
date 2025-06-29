using System;
using TMPro;
using UnityEngine;
using XTools;

namespace Snake {
    public class InteractView : MonoBehaviour {
        
        [SerializeField] TMP_Text _text;
        [SerializeField] GameObject _interactPanel;
        
        InteractComponent _interactComponent;

        void Awake() {
            // _interactPanel.SetActive(false);
        }

        void Start() {
            ServiceLocator.For(this).Get(out PlayerMediator player);
            _interactComponent = player.interactComponent;
            
            _interactComponent.OnEnterInteract += OnEnterInteract;
            // Debug.Log("Interact View");
            _interactPanel.SetActive(false);
        }

        void OnEnterInteract(string obj) {
            // Debug.Log(obj);
            if (obj != null) {
                _text.text = obj;
                _interactPanel.SetActive(true);
            }
            else {
                _interactPanel.SetActive(false);
            }
        }
    }
}