
using System.Collections.Generic;
using RPG.Item;
using UnityEngine;

namespace RPG.Character.Player {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        List<IItem> _items = new();
        public List<IItem> Items {
            get { return _items; }
        }

        public void AddItem(IItem item) {
            Items.Add(item);
        }

        public bool HasItem(IItem item) {
            return _items.Contains(item);
        }

        public void RemoveItem(IItem item) {
            _items.Remove(item);
        }
    }
}