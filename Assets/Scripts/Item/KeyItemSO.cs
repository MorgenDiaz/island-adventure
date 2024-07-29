using UnityEngine;
using UnityEngine.UI;

namespace RPG.Item {
    [CreateAssetMenu(fileName = "Key Item", menuName = "Island Adventure/ Key Item", order = 1)]
    public class KeyItemSO : ItemSO {
        override public ItemType ItemType { get; set; } = ItemType.KeyItem;

        [SerializeField, Tooltip("Item names must be unique to prevent conflicts with other items.")]
        private string _itemName;
        override public string ItemName { get { return _itemName; } set { _itemName = value; } }

        [SerializeField]
        private Sprite _itemImage;
        override public Sprite ItemImage { get { return _itemImage; } set { _itemImage = value; } }
        override public bool Equippable { get; set; } = false;
    }
}