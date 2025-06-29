using UnityEngine;

namespace Snake {
    public class RotationObject : MonoBehaviour {
        
        Camera _camera;
        private Vector3 _lastCameraPosition;

        void Start()
        {
            _camera = Camera.main;
            UpdateRotation();
        }

        void LateUpdate()
        {
            if (!IsCameraMoved()) return;
            
            UpdateRotation();
        }

        void UpdateRotation()
        {
            LookAtCamera();
            SetLastCameraPosition();
        }

        private void LookAtCamera()
        {
            transform.LookAt(_camera.transform);
        }

        private void SetLastCameraPosition()
        {
            _lastCameraPosition = _camera.transform.position;
        }

        private bool IsCameraMoved()
        {
            return _lastCameraPosition != _camera.transform.position;
        }
        
    }
}