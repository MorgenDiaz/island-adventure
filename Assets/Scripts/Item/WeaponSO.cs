using UnityEngine;

namespace RPG.Item {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Island Adventure/ Weapon", order = 1)]
    [System.Serializable]
    public class WeaponSO : ItemSO, IWeapon {
        override public ItemType ItemType { get; set; } = ItemType.Weapon;

        [SerializeField, Tooltip("Item names must be unique to prevent conflicts with other items.")]
        private string _itemName;
        override public string ItemName { get { return _itemName; } set { _itemName = value; } }

        [SerializeField]
        private Sprite _itemImage;
        override public Sprite ItemImage { get { return _itemImage; } set { _itemImage = value; } }

        [SerializeField]
        private int _damage = 0;
        public int Damage { get { return _damage; } set { _damage = value; } }
        [Tooltip("Required to render weapon on the character model.")]

        [SerializeField]
        private string _gameTag = "";
        public string GameTag { get { return _gameTag; } set { _gameTag = value; } }

        [SerializeField]
        private bool _isEquipped = false;

        public bool IsEquipped {
            get { return _isEquipped; }
            set { _isEquipped = value; }
        }

        override public bool Equippable { get; set; } = true;
    }
}