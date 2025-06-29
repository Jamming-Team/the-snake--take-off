using XTools;

namespace Snake {
    public class PlayerMediator {
        
        InteractComponent _interactComponent;
        public InteractComponent interactComponent => _interactComponent;
        PlayerInventory _playerInventory;
        public PlayerInventory playerInventory => _playerInventory;
        
        TransitionComponent _transitionComponent;
        public TransitionComponent transitionComponent => _transitionComponent;

        public void Init(InteractComponent interactComponent, PlayerInventory playerInventory, TransitionComponent transitionComponent) {
            _interactComponent = interactComponent;
            _playerInventory = playerInventory;
            _transitionComponent = transitionComponent;

            ServiceLocator.ForSceneOf(_interactComponent).Register(this);
        }
        
        
        
    }
}