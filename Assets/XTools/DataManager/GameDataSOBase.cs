using UnityEngine;

namespace XTools {
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "XTools/GameDataSO", order = 0)]
    public class GameDataSOBase : ScriptableObject {
        public AudioData audio;
    }
}