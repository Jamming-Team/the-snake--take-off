using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XTools;

namespace Snake {
    public class InteractView : MonoBehaviour {
        
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _image;
        [SerializeField] GameObject _interactPanel;
        [SerializeField] Animator _animator;
        
        
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

        void OnEnterInteract(InteractableData obj) {
            // Debug.Log(obj);
            if (obj != null) {
                // _text.text = obj;
                // _image.sprite = obj.imageToShow;
                _interactPanel.SetActive(true);
                SetAnim(obj.interactType);
                _image.color = obj.color;
                _interactPanel.SetActive(true);
            }
            else {
                _interactPanel.SetActive(false);
            }
        }
        
        
        void SetAnim(InteractType interactType) {

            switch (interactType) {
                case InteractType.PickUp: {
                    _animator.SetTrigger("PickUp");
                    break;
                }
                case InteractType.Push: {
                    _animator.SetTrigger("Push");
                    break;
                }
                case InteractType.Open: {
                    _animator.SetTrigger("Open");
                    break;
                }
                case InteractType.JustOpen: {
                    _animator.SetTrigger("JustOpen");
                    break;
                }
            }
        }
    }


    public enum InteractType {
        PickUp,
        Push,
        Open,
        JustOpen
    }
}