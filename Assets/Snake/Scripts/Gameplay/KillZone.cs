using UnityEngine;
using XTools;

namespace Snake {
    public class KillZone : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                EventBus<KillZoneEntered>.Raise(new KillZoneEntered());
            }
        }
    }
    
    public struct KillZoneEntered : IEvent {}
}