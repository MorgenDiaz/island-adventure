using System.Collections.Generic;
using RPG.Item;
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
            _inventoryPanel = _controller.InventoryContainer.Q("inventory-panel");

            _closeInventoryButton = _controller.InventoryContainer.Q<Button>("close-inventory-button");
            _closeInventoryButton.RegisterCallback<ClickEvent>(HandleCloseInventoryButtonClicked);

            LoadInventoryItemViews();

            CoreSystem.PauseGame();
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.UI);
            _controller.InventoryContainer.style.display = DisplayStyle.Flex;
        }
        private void LoadInventoryItemViews() {
            _inventoryPanel.Clear();
            _controller.InventoryComponent.Items.ForEach(CreateInventoryItem);
        }
        private void HandleCloseInventoryButtonClicked(ClickEvent clickEvent) {
            ExitState();
        }
        public void CreateInventoryItem(IItem item) {
            VisualElement itemContainer = new();
            itemContainer.AddToClassList("inventory-item");
            if (item.Equippable && _controller.EquipmentComponent.IsItemEquipped(item)) {
                itemContainer.AddToClassList("inventory-item-equipped");
            }
            /*
                inside inventory track a set of equipped items
                expose a function is item equipped
                expose eqpip unequipped functions

                instead
                track set of equipped items in equipment component
                when rendering item see if it is equipped and apply style
            */

            VisualElement itemImage = new();
            itemImage.AddToClassList("inventory-item-image");

            if (item.ItemImage) {
                StyleBackground backgroundImage = new(item.ItemImage);
                itemImage.style.backgroundImage = backgroundImage;
            }

            Label itemNameText = new();
            itemNameText.AddToClassList("inventory-item-text");
            itemNameText.text = item.ItemName;

            itemContainer.Add(itemImage);
            itemContainer.Add(itemNameText);

            itemContainer.RegisterCallback<ClickEvent>((ClickEvent clickEvent) => { HandleItemSelected(item); });

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

        private void HandleItemSelected(IItem item) {
            if (item.ItemType == ItemType.Weapon) {
                if (_controller.EquipmentComponent.IsItemEquipped(item)) {
                    _controller.EquipmentComponent.UnequipWeapon();
                }
                else {
                    _controller.EquipmentComponent.EquipWeapon(item as WeaponSO);
                }
            }

            LoadInventoryItemViews();
        }
    }
}