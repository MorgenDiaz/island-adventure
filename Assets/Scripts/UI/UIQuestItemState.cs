using RPG.Quest;
using RPG.Utility;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIQuestItemState : IUIState {
        private readonly UIController _controller;
        private QuestItemSO _questItem;

        private Label _questItemText;
        private VisualElement _questItemImage;
        public QuestItemSO QuestItem {
            get { return _questItem; }
            set { _questItem = value; }
        }
        public UIQuestItemState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            CoreSystem.PauseGame();

            LoadQuestItemUI();

            _controller.QuestItemContainer.style.display = DisplayStyle.Flex;
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.UI);
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
            _questItemText.text = QuestItem.itemName;

            _questItemImage = _controller.QuestItemContainer.Q("quest-item-image");
            if (QuestItem.itemImage) {
                StyleBackground backgroundImage = new(QuestItem.itemImage);
                _questItemImage.style.backgroundImage = backgroundImage;
            }
        }
    }
}