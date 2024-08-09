using System.Collections.Generic;
using RPG.Utility;
using UnityEngine.UIElements;

namespace RPG.UI {
    class UIPauseState : IUIState {
        private readonly UIController _controller;
        private Button _resumeButton;
        public UIPauseState(UIController controller) {
            _controller = controller;
        }

        public void EnterState() {
            _controller.PauseContainer.style.display = DisplayStyle.Flex;

            _resumeButton = _controller.PauseContainer.Q<Button>("resume-button");
            _resumeButton.AddToClassList("active");

            _controller.Buttons = new List<Button> { _resumeButton };
            _resumeButton.RegisterCallback<ClickEvent>(HandleResumeButtonClicked);

            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.UI);

            CoreSystem.PauseGame();
        }

        public void ExitState() {
            _controller.PauseContainer.style.display = DisplayStyle.None;
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.GAMEPLAY);
            CoreSystem.ResumeGame();
        }

        public void SelectButton() {
            ExitState();
        }
        private void HandleResumeButtonClicked(ClickEvent clickEvent) {
            ExitState();
        }

    }
}