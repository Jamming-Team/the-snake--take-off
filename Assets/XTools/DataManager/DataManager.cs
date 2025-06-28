using UnityEngine;

namespace XTools {
    public abstract class DataManager<TData> : IVisitor where TData : GameDataSOBase {
        [SerializeField] TData _gameData;

        public void TrySupply(IVisitable requester) {
            requester.Accept(this);
        }

        // --- Visitors (Data Suppliers) ---

        public void Visit<T>(T o) where T : IVisitable {
            var visitMethod = GetType().GetMethod("Visit", new[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new[] { typeof(object) }))
                visitMethod.Invoke(this, new object[] { o });
            else
                DefaultVisit(o);
        }

        void DefaultVisit(object o) {
            // noop (== `no op` == `no operation`)
            Debug.Log("DataManager.DefaultVisit");
        }
    }
}