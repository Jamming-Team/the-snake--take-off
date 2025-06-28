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
                CastDirection.Forward => Vector3.forward,
                CastDirection.Right => Vector3.right,
                CastDirection.Up => Vector3.up,
                CastDirection.Backward => Vector3.back,
                CastDirection.Left => Vector3.left,
                CastDirection.Down => Vector3.down,
                _ => Vector3.one
            };
        }
    }
}