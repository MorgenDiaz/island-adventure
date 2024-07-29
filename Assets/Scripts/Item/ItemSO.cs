namespace RPG.Item {
    using UnityEngine;

    public abstract class ItemSO : ScriptableObject, IItem {
        public abstract ItemType ItemType { get; set; }

        public abstract string ItemName { get; set; }

        public abstract Sprite ItemImage { get; set; }

        public abstract bool Equippable { get; set; }
    }
}