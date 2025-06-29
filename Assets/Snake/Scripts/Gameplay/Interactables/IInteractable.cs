using System;
using UnityEngine;

namespace Snake {
    public interface IInteractable {

        public void Interact();
        
        public InteractableData GetInteractableData();

    }

    [Serializable]
    public class InteractableData {
        public string textToShow;
        public Sprite imageToShow;
        public InteractType interactType;
        public Color color;
    }
}