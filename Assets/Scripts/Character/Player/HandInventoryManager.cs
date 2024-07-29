using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Character.Player {

    public class HandInventoryManager : MonoBehaviour {
        private Dictionary<string, GameObject> _equipableItems;
        private GameObject _activeItemObject = null;
        private void Awake() {
            _equipableItems = new();

            Transform[] childObjects = GetComponentsInChildren<Transform>(true);

            foreach (Transform childObject in childObjects) {
                _equipableItems[childObject.gameObject.tag] = childObject.gameObject;
            }
        }

        private void OnEnable() {
            EventManager.OnEquipItemRightHand += HandleItemEquip;
            EventManager.OnUnEquipItemRightHand += HandleItemUnEquip;
        }

        private void OnDisable() {
            EventManager.OnEquipItemRightHand -= HandleItemEquip;
            EventManager.OnUnEquipItemRightHand += HandleItemUnEquip;
        }

        private void HandleItemEquip(string itemTag) {
            UnEquipItem();

            GameObject itemToEquip = _equipableItems[itemTag];
            _activeItemObject = itemToEquip;

            if (_activeItemObject != null) {
                _activeItemObject.SetActive(true);
            }
        }

        private void HandleItemUnEquip() {
            UnEquipItem();
        }

        private void UnEquipItem() {
            if (_activeItemObject == null) return;
            _activeItemObject.SetActive(false);
            _activeItemObject = null;
        }
    }
}