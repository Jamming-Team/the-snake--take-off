using System;
using UnityEngine;
using Snake.GA_SM;

namespace Snake {
    // Calculate player's momentum and velocity
    // based on input
    
    // Being able to calculate momentum is the primary purpose of this controller (based on gravity, based on player input)
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerController : MonoBehaviour {
        #region Fields

        [SerializeField] InputReader _input;

        Transform _tr;
        PlayerMover _mover;

        bool _jumpInputIsLocked, _jumpKeyWasPressed, _jumpKeyWasLetGo, _jumpKeyIsPressed;

        public float movementSpeed = 7f;
        public float airControlRate = 2f;
        public float jumpSpeed = 10f;
        public float jumpDuration = 0.2f;
        public float airFriction = 0.5f;
        public float groundFriction = 100f;
        public float gravity = 10f;
        public float slideGravity = 5f;
        public float slopeLimit = 30f;
        public bool useLocalMomentum;

        GA_SM.StateMachine _stateMachine;
        XTools.CountdownTimer _jumpTimer;

        // So we can control our movement based on where is the camera pointing
        // THE THING I NEED
        [SerializeField] Transform _cameraTransform;

        Vector3 _momentum, _savedVelocity, _savedMovementVelocity;
        
        public event Action<Vector3> OnJump = delegate { };
        public event Action<Vector3> OnLand  = delegate { };
        
        #endregion

        void Awake() {
            _tr = transform;
            _mover = GetComponent<PlayerMover>();
            // The hell is ceilingDetector
            
            _jumpTimer = new XTools.CountdownTimer(jumpDuration);
            
            SetupStateMachine();
        }

        void Start() {
            _input.EnablePlayerActions();
        }

        void Update() {
            _stateMachine.Update();
            Debug.Log(_stateMachine.CurrentState);
        }

        void SetupStateMachine() {
            _stateMachine = new GA_SM.StateMachine();

            var grounded = new GroundedState(this);
            var falling = new FallingState(this);
            var sliding = new SlidingState(this);
            var rising = new RisingState(this);
            var jumping = new JumpingState(this);
            
            At(grounded, rising, () => IsRising());
            At(grounded, sliding, () => _mover.IsGrounded() && IsGroundTooSteep());
            At(grounded, falling, () => !_mover.IsGrounded());
            At(grounded, jumping, () => (_jumpKeyIsPressed || _jumpKeyWasPressed) && !_jumpInputIsLocked);
            
            At(falling, rising, () => IsRising());
            At(falling, grounded, () => _mover.IsGrounded() && !IsGroundTooSteep());
            At(falling, sliding, () => _mover.IsGrounded() && IsGroundTooSteep());
            
            At(sliding, rising, () => IsRising());
            At(sliding, falling, () => !_mover.IsGrounded());
            At(sliding, grounded, () => _mover.IsGrounded() && !IsGroundTooSteep());
            
            At(rising, grounded, () => _mover.IsGrounded() && !IsGroundTooSteep());
            At(rising, sliding, () => _mover.IsGrounded() && IsGroundTooSteep());
            At(rising, falling, () => IsFalling());
            
            // TODO Add Jumping Transitions
            
            _stateMachine.SetState(falling);
        }
        
        // At one state and we want to go another state on condition
        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
        // Transition to a state from any other state
        void Any<T>(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

        bool IsRising() => VectorMath.GetDotProduct(GetMomentum(), _tr.up) > 0f;
        bool IsFalling() => VectorMath.GetDotProduct(GetMomentum(), _tr.up) < 0f;
        bool IsGroundTooSteep() => Vector3.Angle(_mover.GetGroundNormal(), _tr.up) > slopeLimit;
        
        public Vector3 GetMomentum() => useLocalMomentum ? _tr.localToWorldMatrix * _momentum : _momentum;

        void FixedUpdate() {
            _mover.CheckForGrounder();
            HandleMomentum();
            // Calculate movement velocity
            Vector3 velocity = _stateMachine.CurrentState is GroundedState ? CalculateMovementVelocity() : Vector3.zero;
            velocity += useLocalMomentum ? _tr.localToWorldMatrix * _momentum : _momentum;
            
            _mover.SetExtendSensorRange(IsGrounded());
            _mover.SetVelocity(velocity);

            _savedVelocity = velocity;
            _savedMovementVelocity = CalculateMovementVelocity();
        }
        
        // Essentially just our movement direction multiplied by speed
        Vector3 CalculateMovementVelocity() => CalculateMovementDirection() * movementSpeed;

        Vector3 CalculateMovementDirection() {
            Vector3 direction = _cameraTransform == null 
                ? _tr.right * _input.direction.x + _tr.forward * _input.direction.y
                : Vector3.ProjectOnPlane(_cameraTransform.right, _tr.up).normalized *  _input.direction.x +
                  Vector3.ProjectOnPlane(_cameraTransform.forward, _tr.up).normalized *  _input.direction.y;
            
            return direction.magnitude > 1f ? direction.normalized : direction;
        }

        void HandleMomentum() {
            if (useLocalMomentum) _momentum = _tr.localToWorldMatrix * _momentum;

            Vector3 verticalMomentum = VectorMath.ExtractDotVector(_momentum, _tr.up);
            Vector3 horizontalMomentum = _momentum -  verticalMomentum;
            
            verticalMomentum -= _tr.up * (gravity * Time.deltaTime);

            if (_stateMachine.CurrentState is GroundedState &&
                VectorMath.GetDotProduct(verticalMomentum, _tr.up) < 0f) {
                verticalMomentum = Vector3.zero;
            }

            if (!IsGrounded()) {
                AdjustHorizontalMomentum(ref horizontalMomentum, CalculateMovementVelocity());
            }

            if (_stateMachine.CurrentState is SlidingState) {
                HandleSliding(ref horizontalMomentum);
            }
            
            float friction = _stateMachine.CurrentState is GroundedState ? groundFriction : airFriction;
            horizontalMomentum = Vector3.MoveTowards(horizontalMomentum, Vector3.zero, friction * Time.deltaTime);
            
            _momentum = horizontalMomentum + verticalMomentum;;
            
            // TODO Handle Jumping
            
            if (useLocalMomentum) _momentum = _tr.worldToLocalMatrix * _momentum;
        }

        void HandleSliding(ref Vector3 horizontalMomentum) {
            Vector3 pointDownVector = Vector3.ProjectOnPlane(_mover.GetGroundNormal(), _tr.up).normalized;
            Vector3 movementVelocity = CalculateMovementVelocity();
            movementVelocity = VectorMath.RemoveDotVector(movementVelocity, pointDownVector);
            horizontalMomentum += movementVelocity * Time.fixedDeltaTime;
            
            _momentum = Vector3.ProjectOnPlane(_momentum, _mover.GetGroundNormal());
            if (VectorMath.GetDotProduct(_momentum, _tr.up) > 0f) _momentum = VectorMath.RemoveDotVector(_momentum, _tr.up);
            
            Vector3 slideDirection = Vector3.ProjectOnPlane(-_tr.up, _mover.GetGroundNormal().normalized);
            _momentum += slideDirection * (slideGravity * Time.deltaTime);
        }
        
        void AdjustHorizontalMomentum(ref Vector3 horizontalMomentum, Vector3 movementVelocity) {
            if (horizontalMomentum.magnitude > movementSpeed) {
                if (VectorMath.GetDotProduct(movementVelocity, horizontalMomentum.normalized) > 0f) {
                    movementVelocity = VectorMath.RemoveDotVector(movementVelocity, horizontalMomentum.normalized);
                }
                    
                horizontalMomentum += movementVelocity * (Time.deltaTime * airControlRate * 0.25f);
            }
            else {
                horizontalMomentum += movementVelocity * (Time.deltaTime * airControlRate);
                horizontalMomentum = Vector3.ClampMagnitude(horizontalMomentum, movementSpeed);
            }
        }
        
        bool IsGrounded()  => _stateMachine.CurrentState is GroundedState or SlidingState;

        
    }
}