using System;
using UnityEngine;

namespace Snake {
    public abstract class ActivatableBase : MonoBehaviour, IActivatable {

        public abstract void Activate();
    }
}