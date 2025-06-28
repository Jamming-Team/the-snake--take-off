using Alchemy.Serialization;
using UnityEngine;

namespace XTools {
    // Don't forget to add it:
    // [CreateAssetMenu(fileName = "GameDataSO", menuName = "XTools/GameDataSO", order = 0)]
    public class GameDataSOBase : ScriptableObject {
        public AudioData audio;
    }
}