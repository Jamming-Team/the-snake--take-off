using System;
using UnityEngine;

namespace Snake {
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerMover : MonoBehaviour {
        #region Fields
        [Header("Collider Settings")] [Range(0f, 1f)] [SerializeField]
        float _stepHeightRatio = 0.1f;

        [SerializeField] float _colliderHeight = 2f;
        [SerializeField] float _colliderThickness = 1f;
        [SerializeField] Vector3 _colliderOffset = Vector3.zero;
        
        Rigidbody _rb;
        Transform _tr;
        CapsuleCollider _col;
        RaycastSensor _sensor;

        bool _isGrounded;
        float _baseSensorRange;
        Vector3 _currentGroundAdjustmentVelocity; // Velocity to adjust player position to maintain ground contact
        int _currentLayer;

        [Header("Sensor Settings")] [SerializeField]
        bool _isInDebugMode;
        bool _isUsingExtendedSensorRange = true; // Use extended range for smother ground transitions
        #endregion


        void Awake() {
            Setup();
            RecalculateColliderDimensions();
        }

        // So we can recalibrate in the editor (while in Play Mode?)
        void OnValidate() {
            if (gameObject.activeInHierarchy) {
                RecalculateColliderDimensions();
            }
        }

        public void CheckForGrounder() {
            if (_currentLayer != gameObject.layer) {
                RecalculateSensorLayerMask();
            }
            
            _currentGroundAdjustmentVelocity = Vector3.zero;
            _sensor.castLength = _isUsingExtendedSensorRange
                ? _baseSensorRange + _colliderHeight * _tr.localScale.x * _stepHeightRatio
                : _baseSensorRange;
            _sensor.Cast();

            _isGrounded = _sensor.HasDetectedHit();
            if (!_isGrounded) return;
            
            float distance = _sensor.GetDistance();
            float upperLimit = _colliderHeight * _tr.localScale.x * (1f - _stepHeightRatio) * 0.5f;
            float middle = upperLimit + _colliderHeight * _tr.localScale.x * _stepHeightRatio;
            float distanceToGo = middle - distance;
            
            _currentGroundAdjustmentVelocity = _tr.up * (distanceToGo / Time.fixedDeltaTime);
        }
        
        public bool IsGrounded() => _isGrounded;
        public Vector3 GetGroundNormal() => _sensor.GetNormal();
        
        public void SetVelocity(Vector3 velocity) => _rb.linearVelocity = velocity + _currentGroundAdjustmentVelocity;
        public void SetExtendSensorRange(bool isExtended) => _isUsingExtendedSensorRange = isExtended;
        
        void Setup() {
            _tr = transform;
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<CapsuleCollider>();
            
            // As we control those things on our own
            _rb.freezeRotation = true;
            _rb.useGravity = false;
            
        }

        void RecalculateSensorLayerMask() {
            int objectLayer = gameObject.layer;
            int layerMask = Physics.AllLayers;

            for (int i = 0; i < 32; i++) {
                if (Physics.GetIgnoreLayerCollision(objectLayer, i)) {
                    layerMask &= ~(1 << i);
                }
            }
            
            int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
            layerMask &= ~(1 << ignoreRaycastLayer);

            _sensor.layerMask = layerMask;
            _currentLayer = objectLayer;
        }
        
        void RecalibrateSensor() {
            _sensor ??= new RaycastSensor(_tr);
            
            _sensor.SetCastOrigin(_col.bounds.center);
            _sensor.SetCastDirection(RaycastSensor.CastDirection.Down);
            RecalculateSensorLayerMask();

            const float safetyDistanceFactor = 0.001f; // Small factor added to prevent clipping issues when the sensor range is calculated
            
            float length = _colliderHeight * (1f - _stepHeightRatio) * 0.5f + _colliderHeight * _stepHeightRatio;
            _baseSensorRange = length * (1f + safetyDistanceFactor) * _tr.localScale.x;
            _sensor.castLength = length * _tr.localScale.x;
        }

        void RecalculateColliderDimensions() {
            if (_col == null) {
                Setup();
            }

            _col.height = _colliderHeight * (1f - _stepHeightRatio);
            _col.radius = _colliderThickness / 2f;
            _col.center = _colliderOffset * _colliderHeight + new Vector3(0f, _stepHeightRatio * _col.height / 2f, 0f);

            if (_col.height / 2f < _col.radius) {
                _col.radius = _col.height / 2f;
            }

            RecalibrateSensor();
        }
    }
}