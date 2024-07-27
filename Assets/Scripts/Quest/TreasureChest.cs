
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Quest {

    public class TreasureChest : MonoBehaviour {
        public Animator AnimatorComponent;
        public QuestItemSO containedItem;
        private bool isInteractable = false;
        private bool isOpen = false;

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
    }
}