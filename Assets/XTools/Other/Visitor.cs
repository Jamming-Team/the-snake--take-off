namespace XTools {
    public interface IVisitor {
        void Visit<T>(T visitable) where T : IVisitable;
    }
    
    public interface IVisitorDataSupplier : IVisitor {
        public void TrySupply(IVisitable visitable);
    }
    
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}