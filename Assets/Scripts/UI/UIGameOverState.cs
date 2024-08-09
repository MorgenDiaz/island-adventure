using System.Collections.Generic;
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIGameOverState : IUIState {
        private readonly UIController _controller;
        private Button _mainMenuButton;
        public UIGameOverState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            _controller.GameOverContainer.style.display = DisplayStyle.Flex;

            _mainMenuButton = _controller.GameOverContainer.Q<Button>("main-menu-button");
            _controller.Buttons = new List<Button> { _mainMenuButton };
            _mainMenuButton.RegisterCallback<ClickEvent>(HandleMainMenuButtonClicked);

            _controller.AudioSourceComponent.clip = _controller.GameOverSound;
            _controller.AudioSourceComponent.Play();

            CoreSystem.PauseGame();
        }
        public void ExitState() {
            _controller.VictoryContainer.style.display = DisplayStyle.None;
        }

        public void SelectButton() {
            GoToMainMenu();
        }

        private void HandleMainMenuButtonClicked(ClickEvent clickEvent) {
            GoToMainMenu();
        }
        private void GoToMainMenu() {
            _controller.StartCoroutine(SceneTransition.Initiate(Constants.Scenes.MAIN_MENU));
        }

    }
}