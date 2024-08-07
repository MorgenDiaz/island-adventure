
using RPG.Item;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIQuestItemState : IUIState {
        private readonly UIController _controller;
        private IItem _questItem;

        private Label _questItemText;
        private VisualElement _questItemImage;

        private VisualElement _itemContainer;
        private Button _claimItemButton;
        public IItem QuestItem {
            get { return _questItem; }
            set { _questItem = value; }
        }
        public UIQuestItemState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            _itemContainer = _controller.QuestItemContainer.Q("item-container");
            _claimItemButton = _itemContainer.Q<Button>("claim-button");

            _claimItemButton.RegisterCallback<ClickEvent>(HandleClaimItemButtonClick);

            CoreSystem.PauseGame();

            LoadQuestItemUI();


            _controller.QuestItemContainer.style.display = DisplayStyle.Flex;
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.UI);
        }

        private void HandleClaimItemButtonClick(ClickEvent clickEvent) {
            _controller.InventoryComponent.AddItem(QuestItem);
            _controller.AudioSourceComponent.clip = _controller.RewardSound;
            _controller.AudioSourceComponent.Play();

            ExitState();
        }

        public void ExitState() {
            _controller.QuestItemContainer.style.display = DisplayStyle.None;
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.GAMEPLAY);
            CoreSystem.ResumeGame();
        }

        public void SelectButton() {
            throw new System.NotImplementedException();
        }
        private void LoadQuestItemUI() {
            _questItemText = _controller.QuestItemContainer.Q<Label>("quest-item-name");
            _questItemText.text = QuestItem.ItemName;

            _questItemImage = _controller.QuestItemContainer.Q("quest-item-image");
            if (QuestItem.ItemImage) {
                StyleBackground backgroundImage = new(QuestItem.ItemImage);
                _questItemImage.style.backgroundImage = backgroundImage;
            }
        }
    }
}