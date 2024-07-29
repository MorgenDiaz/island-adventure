using UnityEngine;

namespace RPG.Item {

    public enum ItemType {
        KeyItem,
        Weapon
    }
    public interface IItem {
        ItemType ItemType { get; set; }
        string ItemName { get; set; }
        Sprite ItemImage { get; set; }

        bool Equippable { get; set; }
    }
}