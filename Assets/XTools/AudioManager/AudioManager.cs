using System;
using UnityEngine;
using UnityEngine.Audio;

namespace XTools {
    public class AudioManager : MonoBehaviour, IVisitable {
        const string MUSIC_VOLUME_NAME = "MusicVolume";
        const string SFX_VOLUME_NAME = "SfxVolume";

        [SerializeField] AudioMixer _mixer;
        [SerializeField] AudioSource[] _musicSources = new AudioSource[2];
        [SerializeField] SoundModel _soundModel;

        AudioData _data;
        MusicModel _musicModel;
        bool _initialized = false;
        EventBinding<DataChanged> _dataChangedBinding;


        void Start() {
            ServiceLocator.For(this).Get(out IVisitorDataSupplier dataManager);
            dataManager.TrySupply(this);

            AdjustMixerVolume();

            // _musicModel = new MusicModel(_data.music,
            //     new MusicModel.MusicSourcesPair { sourceOne = _musicSources[0], sourceTwo = _musicSources[1] });

            // Subscribe the data event

            _initialized = true;
            
            // PlayMusic(MusicBundleType.MainMenu, true);
            
            _dataChangedBinding = new EventBinding<DataChanged>(AdjustMixerVolume);
            EventBus<DataChanged>.Register(_dataChangedBinding);
        }

        void OnDestroy() {
            // Unsubscribe the data event
        }

        void Update() {
            if (!_initialized) return;

            // _musicModel.CheckForCrossFade();
        }

        // --- Interface ---

        public SoundEmitter PlaySound(SoundData soundData, Transform playTransform = null) {
            if (!_soundModel.initialized) return null;

            var a = _soundModel.CreateSoundBuilder().WithRandomPitch();
            if (playTransform != null) {
                a.WithPosition(playTransform.position);
            }

            return a.Play(soundData);
        }

        public void PlayMusic(MusicBundleType type, bool skipCheck = false) {
            if (!_initialized) return;

            var flag = _musicModel.LoadBundle(type);

            if (!skipCheck && flag) return;

            _musicModel.PlayNextTrack();
        }

        // --- Setup ---

        void AdjustMixerVolume() {
            _mixer.SetFloat(SFX_VOLUME_NAME, _data.sfxVolume.ToLogarithmicVolume());
            _mixer.SetFloat(MUSIC_VOLUME_NAME, _data.musicVolume.ToLogarithmicVolume());
        }

        public void SetData(AudioData data) {
            _data = data;
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}