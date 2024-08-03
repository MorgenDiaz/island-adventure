using UnityEngine;

namespace RPG.Item {
    public class KeyItem : IKeyItem {
        public ItemType ItemType { get; set; } = ItemType.KeyItem;

        private string _itemName;
        public string ItemName { get { return _itemName; } set { _itemName = value; } }

        private Sprite _itemImage;
        public Sprite ItemImage { get { return _itemImage; } set { _itemImage = value; } }
        public bool Equippable { get; set; } = false;
    }
}