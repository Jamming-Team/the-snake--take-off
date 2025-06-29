using System;
using UnityEngine;
using XTools;

namespace Snake {
    public class FinishZone : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                EventBus<GameFinished>.Raise(new GameFinished());
            }
        }
    }
    
    public struct GameFinished : IEvent {}
    
}