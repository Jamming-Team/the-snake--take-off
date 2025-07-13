namespace Snake.GA_SM {
    public interface ITransition {
        IState to { get; }
        IPredicate condition { get; }
    }
}