using UnityEngine;
using UnityEngine.UI;

namespace RPG.Quest {
    [CreateAssetMenu(fileName = "Quest Item", menuName = "Island Adventure/ Quest Item", order = 1)]
    public class QuestItemSO : ScriptableObject {
        [Tooltip("Item names must be unique to prevent conflicts with other items.")]
        public string itemName;
        public Sprite itemImage;
    }
}