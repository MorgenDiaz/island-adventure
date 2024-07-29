using UnityEngine;

namespace RPG.Item {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Island Adventure/ Weapon", order = 1)]
    public class WeaponSO : ItemSO {
        override public ItemType ItemType { get; set; } = ItemType.Weapon;

        [SerializeField, Tooltip("Item names must be unique to prevent conflicts with other items.")]
        private string _itemName;
        override public string ItemName { get { return _itemName; } set { _itemName = value; } }

        [SerializeField]
        private Sprite _itemImage;
        override public Sprite ItemImage { get { return _itemImage; } set { _itemImage = value; } }

        public int Damage = 0;
        [Tooltip("Required to render weapon on the character model.")]
        public string GameTag;

        private bool _isEquipped;

        public bool Equipped {
            get { return _isEquipped; }
            set { _isEquipped = value; }
        }

        override public bool Equippable { get; set; } = true;
    }
}