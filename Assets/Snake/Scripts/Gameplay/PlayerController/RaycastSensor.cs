using UnityEngine;

namespace Snake {
    
    // Cast a ray to see if we hit something
    public class RaycastSensor {
        public float castLength = 1f;
        public LayerMask layerMask = 255;
        
        Vector3 _origin = Vector3.zero;
        Transform _tr;
        
        public enum CastDirection {Forward, Right, Up, Backward, Left, Down}
        CastDirection _castDirection;
        
        RaycastHit _hitInfo;

        public RaycastSensor(Transform playerTransform) {
            _tr = playerTransform;
        }

        public void Cast() {
            Vector3 worldOrigin = _tr.TransformPoint(_origin);
            Vector3 worldDirection = GetCastDirection();
            
            // He ignores casting to Triggers here
            Physics.Raycast(worldOrigin, worldDirection, out _hitInfo, castLength, layerMask, QueryTriggerInteraction.Ignore);
        }
        
        public bool HasDetectedHit() => _hitInfo.collider != null;
        public float GetDistance() => _hitInfo.distance;
        public Vector3 GetNormal() => _hitInfo.normal;
        public Vector3 GetPosition() => _hitInfo.point;
        public Collider GetCollider() => _hitInfo.collider;
        public Transform GetTransform() => _hitInfo.transform;
        
        public void SetCastDirection(CastDirection direction) => _castDirection = direction;
        // Convert ws position into the local space of the transform we stored
        public void SetCastOrigin(Vector3 pos)  => _origin = _tr.InverseTransformPoint(pos);
        
        Vector3 GetCastDirection() {
            // This is an example of Switch Expression
            return _castDirection switch {
                CastDirection.Forward => _tr.forward,
                CastDirection.Right => _tr.right,
                CastDirection.Up => _tr.up,
                CastDirection.Backward => -_tr.forward,
                CastDirection.Left => _tr.right,
                CastDirection.Down => -_tr.up,
                _ => Vector3.one
            };
        }
        
        public void DrawDebug() {
            if (!HasDetectedHit()) return;

            Debug.DrawRay(_hitInfo.point, _hitInfo.normal, Color.red, Time.deltaTime);
            float markerSize = 0.2f;
            Debug.DrawLine(_hitInfo.point + Vector3.up * markerSize, _hitInfo.point - Vector3.up * markerSize, Color.green, Time.deltaTime);
            Debug.DrawLine(_hitInfo.point + Vector3.right * markerSize, _hitInfo.point - Vector3.right * markerSize, Color.green, Time.deltaTime);
            Debug.DrawLine(_hitInfo.point + Vector3.forward * markerSize, _hitInfo.point - Vector3.forward * markerSize, Color.green, Time.deltaTime);
        }
    }
}