using XTools;

namespace Snake {
    public class PlayerMediator {
        
        InteractComponent _interactComponent;
        public InteractComponent interactComponent => _interactComponent;
        PlayerInventory _playerInventory;
        public PlayerInventory playerInventory => _playerInventory;

        public void Init(InteractComponent interactComponent, PlayerInventory playerInventory) {
            _interactComponent = interactComponent;
            _playerInventory = playerInventory;

            ServiceLocator.ForSceneOf(_interactComponent).Register(this);
        }
        
        
        
    }
}