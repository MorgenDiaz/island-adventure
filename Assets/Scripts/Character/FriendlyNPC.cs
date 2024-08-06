using Ink.Runtime;
using RPG.Character.Player;
using RPG.Core;
using RPG.Item;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RPG.Character {
    public class FriendlyNPC : MonoBehaviour, ISaveable {
        private UniqueID _uniqueIDComponent;

        [SerializeField]
        private GameObject _interactionIndicator;

        [SerializeField]
        private GameObject _dialogIndicator;

        [SerializeField]
        TextAsset InkDialogue;

        [SerializeField]
        private ItemSO _requiredQuestItem;
        [SerializeField]
        private ItemSO _playerRewardItem;
        private Story _story;

        private bool _isPlayerInInteractionRange = false;

        private bool isQuestComplete = false;

        private void Awake() {
            EventManager.TriggerOnRegisterSaveable(this);

            if (InkDialogue == null) {
                Debug.LogWarning($"Npc {name} requires an ink TextAsset");
                return;
            }

            if (!TryGetComponent<UniqueID>(out _uniqueIDComponent)) {
                Debug.LogWarning($"{name} must have a UniqueID component to persist its state across screen transitions.");
            }

            _dialogIndicator.SetActive(false);
            Load();
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

                playerInventory.RemoveItem(_requiredQuestItem);
                playerInventory.AddItem(_playerRewardItem);
            }

            return true;
        }

        public void Save() {
            PlayerPrefs.SetInt($"npc_{_uniqueIDComponent.ID}_is_quest_complete", isQuestComplete ? 1 : 0);
        }

        public void Load() {
            isQuestComplete = PlayerPrefs.GetInt($"npc_{_uniqueIDComponent.ID}_is_quest_complete", 0) == 1;
        }
    }
}