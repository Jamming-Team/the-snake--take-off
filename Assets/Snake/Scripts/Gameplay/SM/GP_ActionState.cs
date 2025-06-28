using XTools;

namespace Snake {
    public class GP_ActionState : GP_SceneState{
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