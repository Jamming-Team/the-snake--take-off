using System;
using System.Collections.Generic;
using UnityEngine;
using XTools;

namespace Snake {
    public class InventoryView : MonoBehaviour {
        [SerializeField] GameObject _gridRoot;
        [SerializeField] InventoryGridItem _gridItemPrefab;

        readonly List<InventoryGridItem> _items = new List<InventoryGridItem>();
        PlayerMediator _mediator;
        
        void Start() {
            ServiceLocator.For(this).Get(out _mediator);
            _mediator.playerInventory.ItemAdded += ItemAdded;
            _mediator.playerInventory.ItemRemoved += ItemRemoved;
        }

        void OnDestroy() {
            _mediator.playerInventory.ItemAdded -= ItemAdded;
            _mediator.playerInventory.ItemRemoved -= ItemRemoved;
        }

        void ItemAdded(InventoryItem obj) {
            Debug.Log("ItemAdded");
            var item  = Instantiate(_gridItemPrefab, _gridRoot.transform);
            item.image.sprite =  obj.sprite;
            _items.Add(item);
        }

        void ItemRemoved(InventoryItem obj) {
            var item = _items.Find(x => x.itemName == obj.name);
            _items.Remove(item);
            Destroy(item.gameObject);
        }


    }
}