using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XTools;

namespace Snake {
    public class PlayerInventory {
        public Action<InventoryItem> ItemAdded = delegate { };
        public Action<InventoryItem> ItemRemoved = delegate { };
        
        readonly List<InventoryItem> _items = new();

        public void Add(InventoryItem item) {
            _items.Add(item);
        }

        public bool TryUse(string name) {
            var foundItem = _items.Find(x => x.name == name);

            if (foundItem != null) {
                _items.Remove(foundItem);
                return true;
            }
            
            return false;
        }
    }
}