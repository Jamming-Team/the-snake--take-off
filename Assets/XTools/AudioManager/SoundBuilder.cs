using UnityEngine;

namespace XTools {
    public class SoundBuilder {
        readonly SoundManager _soundManager;
        Vector3 _position = Vector3.zero;
        bool _randomPitch;

        public SoundBuilder(SoundManager soundManager) {
            _soundManager = soundManager;
        }

        public SoundBuilder WithPosition(Vector3 position) {
            _position = position;
            return this;
        }

        public SoundBuilder WithRandomPitch() {
            _randomPitch = true;
            return this;
        }

        public SoundEmitter Play(SoundData soundData) {
            if (soundData == null) {
                Debug.LogError("SoundData is null");
                return null;
            }

            if (!_soundManager.CanPlaySound(soundData)) return null;

            SoundEmitter soundEmitter = _soundManager.Get();
            soundEmitter.Initialize(soundData, _soundManager);
            soundEmitter.transform.position = _position;
            soundEmitter.transform.parent = _soundManager.transform;

            if (_randomPitch) soundEmitter.WithRandomPitch();

            if (soundData.frequentSound) soundEmitter.node = _soundManager.frequentSoundEmitters.AddLast(soundEmitter);

            soundEmitter.Play();

            return soundEmitter;
        }
    }
}