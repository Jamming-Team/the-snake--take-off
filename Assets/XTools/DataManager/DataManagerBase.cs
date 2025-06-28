using UnityEngine;

namespace XTools {
    public class DataManagerBase<TData> : MonoBehaviour, IVisitor where TData : GameDataSOBase {
        [SerializeField] TData _gameData;

        void Awake() {
            ServiceLocator.Global.Register(this);
        }

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

        void Visit(AudioManager requester) {
            requester.SetData(_gameData.audio);
        } 
    }
}