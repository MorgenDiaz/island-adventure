using System.Collections.Generic;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIInventoryState : IUIState {
        private readonly UIController _controller;
        private VisualElement _inventoryPanel;
        private Button _closeInventoryButton;
        public UIInventoryState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            /*
            *init inventory panel
            *clear children
            dynamically generate inventory items from player inventory component
            *switch action map 
            *pause time
            *show inventory-container
            */
            _inventoryPanel = _controller.InventoryContainer.Q("inventory-panel");
            _inventoryPanel.Clear();

            _closeInventoryButton = _controller.InventoryContainer.Q<Button>("close-inventory-button");
            _closeInventoryButton.RegisterCallback<ClickEvent>(HandleCloseInventoryButtonClicked);

            _controller.InventoryComponent.Items.ForEach(CreateInventoryItem);

            CoreSystem.PauseGame();
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.UI);
            _controller.InventoryContainer.style.display = DisplayStyle.Flex;
        }

        private void HandleCloseInventoryButtonClicked(ClickEvent clickEvent) {
            ExitState();
        }

        public void CreateInventoryItem(QuestItemSO item) {
            VisualElement itemContainer = new();
            itemContainer.AddToClassList("inventory-item");

            VisualElement itemImage = new();
            itemImage.AddToClassList("inventory-item-image");

            if (item.itemImage) {
                StyleBackground backgroundImage = new(item.itemImage);
                itemImage.style.backgroundImage = backgroundImage;
            }

            Label itemNameText = new();
            itemNameText.AddToClassList("inventory-item-text");
            itemNameText.text = item.itemName;

            itemContainer.Add(itemImage);
            itemContainer.Add(itemNameText);

            _inventoryPanel.Add(itemContainer);
        }

        public void ExitState() {
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.GAMEPLAY);
            _controller.InventoryContainer.style.display = DisplayStyle.None;
            CoreSystem.ResumeGame();
        }

        public void SelectButton() {
            throw new System.NotImplementedException();
        }
    }
}