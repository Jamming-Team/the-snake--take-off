using UnityEngine;
using XTools;

namespace Snake {
    public class GP_ActionState : GP_SceneState{
        
        protected override void OnEnter() {
            base.OnEnter();
            
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
        
        protected override void OnExit() {
            base.OnExit();
            
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
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