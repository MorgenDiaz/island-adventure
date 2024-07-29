using Ink.Runtime;
using RPG.Character.Player;
using RPG.Core;
using RPG.Quest;
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

        [SerializeField]
        private QuestItemSO _requiredQuestItem;
        private Story _story;

        private bool _isPlayerInInteractionRange = false;

        private bool isQuestComplete = false;

        private void Awake() {
            _dialogIndicator.SetActive(false);

            if (InkDialogue == null) {
                Debug.LogWarning($"Npc {name} requires an ink TextAsset");
                return;
            }
            _story = new Story(InkDialogue.text);

        }
        private void OnTriggerEnter(Collider other) {
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

            if (isQuestComplete) {
                _story.ChoosePathString("postCompletion");
            }
            EventManager.TriggerInitiateDialogue(_story, VerifyPlayerHasDesiredItem);
        }

        public object VerifyPlayerHasDesiredItem() {
            GameObject player = GameObject.FindWithTag(Constants.Tags.PLAYER);
            Inventory playerInventory = player.GetComponent<Inventory>();
            bool playerHasItem = playerInventory.HasItem(_requiredQuestItem);

            _story.variablesState["questCompleted"] = playerHasItem;

            if (playerHasItem) {
                isQuestComplete = true;
            }

            return true;
        }
    }
}