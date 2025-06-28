using XTools;

namespace Snake {
    public class GP_SceneState : SceneState<GP_SceneController> {
    protected override void OnUIButtonPressed(UIButtonPressed evt) {
        switch (evt.buttonType) {
            case UIButtonPressed.UIButtons.LoadMainMenu: {
                _sceneLoader.TryLoadScene(GameConstants.SceneNames.MAIN_MENU);
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