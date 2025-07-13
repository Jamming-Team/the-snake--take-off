using UnityEngine;
using XTools;

namespace Snake {
    public class RotationObject : MonoBehaviour {
        InteractComponent _interactComponent;

        Vector3 _lastPosition;

        void Start() {
            ServiceLocator.For(this).Get(out PlayerMediator mediator);
            _interactComponent = mediator.interactComponent;
            
            UpdateRotation();
        }

        void LateUpdate() {
            if (!IsCameraMoved()) return;

            UpdateRotation();
        }

        void UpdateRotation() {
            LookAtCamera();
            SetLastCameraPosition();
        }

        void LookAtCamera() {
            transform.LookAt(_interactComponent.transform);
        }

        void SetLastCameraPosition() {
            _lastPosition = _interactComponent.transform.position;
        }

        bool IsCameraMoved() {
            return _lastPosition != _interactComponent.transform.position;
        }
    }
}