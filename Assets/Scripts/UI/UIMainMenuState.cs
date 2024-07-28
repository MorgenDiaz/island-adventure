using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIMainMenuState : IUIState {
        private readonly UIController _controller;
        public UIMainMenuState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            _controller.MainMenuContainer.style.display = DisplayStyle.Flex;
            _controller.Buttons = _controller.MainMenuContainer.Query<Button>(null, "menu-button").ToList();

            Button startButton = _controller.DocumentRoot.Query<Button>("start-button");
            startButton.RegisterCallback<ClickEvent>(HandleStartButtonClicked);
            _controller.Buttons[0].AddToClassList("active");
        }
        public void SelectButton() {
            Button button = _controller.Buttons[_controller.CurrentSelection];
            if (button.name == "start-button") {
                SceneTransition.Initiate(Constants.Scenes.ISLAND);
            }
        }
        public void ExitState() {
            _controller.MainMenuContainer.style.display = DisplayStyle.None;
        }
        private void HandleStartButtonClicked(ClickEvent clickEvent) {
            SceneTransition.Initiate(Constants.Scenes.ISLAND);
        }
    }
}