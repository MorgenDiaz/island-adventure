
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Item {

    public class TreasureChest : MonoBehaviour, ISaveable {
        private UniqueID _uniqueIDComponent;
        public Animator AnimatorComponent;
        public ItemSO containedItem;
        private bool isInteractable = false;
        private bool isOpen = false;
        void Awake() {
            if (!TryGetComponent<UniqueID>(out _uniqueIDComponent)) {
                Debug.LogWarning($"{name} must have a UniqueID component to persist its state across screen transitions.");
            }

            EventManager.TriggerOnRegisterSaveable(this);

            Load();
        }
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag(Constants.Tags.PLAYER)) {
                isInteractable = true;
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other.CompareTag(Constants.Tags.PLAYER)) {
                isInteractable = false;
            }
        }

        public void HandleInteraction(InputAction.CallbackContext context) {
            if (!isInteractable || isOpen || !context.performed) return;

            EventManager.TriggerReceiveQuestItem(containedItem);
            AnimatorComponent.SetBool(Constants.AnimatorParams.IS_SHAKING, false);
            isOpen = true;
        }

        public void Save() {
            PlayerPrefs.SetInt($"chest_{_uniqueIDComponent.ID}_is_open", isOpen ? 1 : 0);
        }

        public void Load() {
            isOpen = PlayerPrefs.GetInt($"chest_{_uniqueIDComponent.ID}_is_open", 0) == 1;

            if (isOpen) {
                AnimatorComponent.SetBool(Constants.AnimatorParams.IS_SHAKING, false);
            }
        }
    }
}