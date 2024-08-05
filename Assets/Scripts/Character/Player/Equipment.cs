using System.Collections.Generic;
using Newtonsoft.Json;
using RPG.Core;
using RPG.Item;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character.Player {
    [RequireComponent(typeof(PlayerCombat))]
    public class Equipment : MonoBehaviour, ISaveable {
        private IWeapon _equippedWeapon;
        private PlayerCombat _combatComponent;

        private void Awake() {
            EventManager.TriggerOnRegisterSaveable(this);

            _combatComponent = GetComponent<PlayerCombat>();
            Load();

            if (_equippedWeapon == null) return;
            EquipWeapon(_equippedWeapon);
        }

        public void EquipWeapon(IWeapon weapon) {
            if (_equippedWeapon != null) {
                UnequipWeapon();
            }
            _equippedWeapon = weapon;
            EventManager.TriggerEquipItemRightHand(_equippedWeapon.GameTag);
            _combatComponent.Damage += _equippedWeapon.Damage;
        }

        public void UnequipWeapon() {
            _combatComponent.Damage -= _equippedWeapon.Damage;
            EventManager.TriggerUnEquipItemRightHand();
            _equippedWeapon = null;
        }

        public bool IsItemEquipped(IItem item) {
            //Need to extend this logic if adding different types of equippable items.
            return _equippedWeapon != null && _equippedWeapon.ItemName == item.ItemName;
        }

        public void Save() {
            JsonSerializerSettings settings = new() {
                Converters = new List<JsonConverter> { new JsonSpriteConverter() }
            };

            string json = JsonConvert.SerializeObject(_equippedWeapon, settings);

            PlayerPrefs.SetString("equipped_weapon", json);
            PlayerPrefs.Save();
        }

        public void Load() {
            if (!PlayerPrefs.HasKey("equipped_weapon")) return;

            JsonSerializerSettings settings = new() {
                Converters = new List<JsonConverter> { new JsonItemConverter() }
            };

            string json = PlayerPrefs.GetString("equipped_weapon");

            _equippedWeapon = JsonConvert.DeserializeObject<Weapon>(json, settings);
        }
    }
}