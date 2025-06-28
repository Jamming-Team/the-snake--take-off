using System;
using UnityEngine;

namespace XTools {
    public class DataManagerBase<TData> : MonoBehaviour, IVisitorDataSupplier where TData : GameDataSOBase {
        [SerializeField] TData _gameData;

        EventBinding<UIAudioSliderChanged> _UiAudioSliderChangedBinding;


        protected virtual void Awake() {
            ServiceLocator.Global.Register(typeof(IVisitorDataSupplier),this);
            
            _UiAudioSliderChangedBinding = new EventBinding<UIAudioSliderChanged>(UiAudioSliderChanged);
            EventBus<UIAudioSliderChanged>.Register(_UiAudioSliderChangedBinding);
        }

        public void TrySupply(IVisitable requester) {
            requester.Accept(this);
        }
        
        void UiAudioSliderChanged(UIAudioSliderChanged evt) {
            switch (evt.audioSliderType) {
                case UIAudioSliderChanged.UIAudioSliders.MusicVolume: {
                    _gameData.audio.musicVolume = evt.value;
                    break;
                }
                case UIAudioSliderChanged.UIAudioSliders.SfxVolume: {
                    _gameData.audio.sfxVolume = evt.value;
                    break;
                }
            }
            EventBus<DataChanged>.Raise(new DataChanged());
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

        public void Visit(AudioManager requester) {
            requester.SetData(_gameData.audio);
        } 
    }
}