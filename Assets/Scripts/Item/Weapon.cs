using UnityEngine;

namespace RPG.Item {
    public class Weapon : IWeapon {
        public ItemType ItemType { get; set; } = ItemType.Weapon;
        private string _itemName;
        public string ItemName { get { return _itemName; } set { _itemName = value; } }

        private Sprite _itemImage;
        public Sprite ItemImage { get { return _itemImage; } set { _itemImage = value; } }

        public int Damage { get; set; } = 0;
        public string GameTag { get; set; }

        private bool _isEquipped = false;

        public bool IsEquipped {
            get { return _isEquipped; }
            set { _isEquipped = value; }
        }

        public bool Equippable { get; set; } = true;
    }
}