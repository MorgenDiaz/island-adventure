using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character {
    public class FriendlyNPC : MonoBehaviour {
        [SerializeField]
        private GameObject _interactionIndicator;
        [SerializeField]
        private GameObject _dialogIndicator;

        [SerializeField]
        TextAsset InkDialogue;

        private bool _isPlayerInInteractionRange = false;

        private void Awake() {
            _dialogIndicator.SetActive(false);
        }
        private void OnTriggerEnter(Collider other) {
            Debug.Log("Did enter notify range");
            if (other.CompareTag(Constants.Tags.PLAYER)) {
                _interactionIndicator.SetActive(false);
                _dialogIndicator.SetActive(true);
                _isPlayerInInteractionRange = true;
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other.CompareTag(Constants.Tags.PLAYER)) {
                _dialogIndicator.SetActive(false);
                _interactionIndicator.SetActive(true);
                _isPlayerInInteractionRange = false;
            }
        }

        public void HandleInteraction(InputAction.CallbackContext context) {
            if (!context.performed || !_isPlayerInInteractionRange) return;
            if (InkDialogue == null) return;

            print("Interact Yo!");
        }
    }
}