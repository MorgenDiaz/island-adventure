using RPG.Core;
using RPG.Item;
using UnityEngine;

namespace RPG.Character.Player {
    [RequireComponent(typeof(PlayerCombat))]
    public class Equipment : MonoBehaviour {
        private WeaponSO _equippedWeapon;
        private PlayerCombat _combatComponent;

        private void Awake() {
            _combatComponent = GetComponent<PlayerCombat>();
        }

        public void EquipWeapon(WeaponSO weapon) {
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
            return _equippedWeapon == (item as WeaponSO);
        }
    }
}