
using System.Collections.Generic;
using RPG.Quest;
using UnityEngine;

namespace RPG.Character.Player {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        List<QuestItemSO> _items;
        public List<QuestItemSO> Items {
            get { return _items; }
        }

        public void AddItem(QuestItemSO item) {
            Items.Add(item);
        }
    }
}