using UnityEngine;

namespace Snake {
    // Calculate player's momentum and velocity
    // based on input
    public class PlayerController {
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

        #endregion
    }
}