using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using UnityEngine;

namespace XTools {
    [Serializable]
    public class AudioData {
        public float musicVolume;
        public float sfxVolume;
        public MusicData music;
    }

    [Serializable]
    public class MusicData {
        // public List<AudioClip> audioClips;
        [AlchemySerializeField] [NonSerialized]
        public Dictionary<MusicBundleType, MusicBundle> bundles = new();

        public float crossFadeTime = 2.0f;
    }

    [Serializable]
    public class MusicBundle {
        public List<AudioClip> audioClips;
        public bool shouldLoopFirstClip;
    }

    public enum MusicBundleType {
        MainMenu,
        Gameplay
    }
}