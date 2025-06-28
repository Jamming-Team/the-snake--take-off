using UnityEngine;
using Snake.GA_SM;

namespace Snake {
    public class GroundedState : IState {
        readonly PlayerController controller;
        

        public GroundedState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            
            Debug.Log("Entered GroundedState"); 
            
            
            controller.OnGroundContactRegained();
        }
    }

    public class FallingState : IState {
        readonly PlayerController controller;

        public FallingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            
            Debug.Log("Entered FallingState");
            controller.OnFallStart();
        }
    }

    public class SlidingState : IState {
        readonly PlayerController controller;

        public SlidingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            
            Debug.Log("Entered SlidingState");
            controller.OnGroundContactLost();
        }
    }

    public class RisingState : IState {
        readonly PlayerController controller;

        public RisingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            
            Debug.Log("Entered RisingState");
            controller.OnGroundContactLost();
        }
    }

    public class JumpingState : IState {
        readonly PlayerController controller;

        public JumpingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            
            Debug.Log("Entered JumpingState");
            controller.OnGroundContactLost();
            controller.OnJumpStart();
        }
    }
}