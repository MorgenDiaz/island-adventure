using System.Collections.Generic;
using RPG.Core;
using RPG.Utility;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIVictoryState : IUIState {
        private readonly UIController _controller;
        private Button _mainMenuButton;
        public UIVictoryState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            _controller.VictoryContainer.style.display = DisplayStyle.Flex;

            _mainMenuButton = _controller.VictoryContainer.Q<Button>("main-menu-button");
            _controller.Buttons = new List<Button> { _mainMenuButton };
            _mainMenuButton.RegisterCallback<ClickEvent>(HandleMainMenuButtonClicked);

            _controller.AudioSourceComponent.clip = _controller.VictorySound;
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
            SceneTransition.Initiate(Constants.Scenes.MAIN_MENU);
        }
    }
}