using UnityEngine;
using XTools;

namespace Snake {
    public class GP_ActionState : GP_SceneState{
        
        EventBinding<GameFinished> _gameFinishedBinding;
        
        protected override void OnEnter() {
            base.OnEnter();
            
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            
            _gameFinishedBinding = new EventBinding<GameFinished>(FinishGame);
            EventBus<GameFinished>.Register(_gameFinishedBinding);
        }
        
        protected override void OnExit() {
            base.OnExit();
            
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            
            EventBus<GameFinished>.Deregister(_gameFinishedBinding);
        }

        void FinishGame() {
            RequestTransition<GP_PostGameState>();
        }
        
        protected override void OnUIButtonPressed(UIButtonPressed evt) {
            base.OnUIButtonPressed(evt);
            switch (evt.buttonType) {
                case UIButtonPressed.UIButtons.Pause: {
                    RequestTransition<GP_PauseState>();
                    break;
                }
            }
        }
        
    }
}