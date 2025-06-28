using XTools;

namespace Snake.MainMenu.SM {
    public class MM_SceneState : SceneState<MM_SceneController> {
        protected override void OnUIButtonPressed(UIButtonPressed evt) {
            switch (evt.buttonType) {
                case UIButtonPressed.UIButtons.Back: {
                    RequestTransition<MainViewState>();
                    break;
                }
                case UIButtonPressed.UIButtons.Settings: {
                    RequestTransition<SettingsViewState>();
                    break;
                }
                case UIButtonPressed.UIButtons.LoadGameplay: {
                    _sceneLoader.TryLoadScene(GameConstants.SceneNames.GAMEPLAY);
                    break;
                }
            }
        }
        
    }
}