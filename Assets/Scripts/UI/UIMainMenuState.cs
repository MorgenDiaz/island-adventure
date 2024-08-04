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

            if (PlayerPrefs.HasKey("scene_id")) {
                RenderContinueButton();
            }
        }

        private void RenderContinueButton() {
            VisualElement buttonContainer = _controller.MainMenuContainer.Q("button-container");

            Button continueButton = new() {
                text = "Continue Game",
                name = "continue-button"
            };

            continueButton.AddToClassList("menu-button");
            continueButton.AddToClassList("primary-button");

            buttonContainer.Add(continueButton);
            _controller.Buttons.Add(continueButton);
        }
        public void SelectButton() {
            Button button = _controller.Buttons[_controller.CurrentSelection];

            if (button.name == "start-button") {
                StartNewGame();
            }
            else if (button.name == "continue-button") {
                int savedScene = PlayerPrefs.GetInt("scene_id");
                SceneTransition.Initiate(savedScene);
            }
        }
        public void ExitState() {
            _controller.MainMenuContainer.style.display = DisplayStyle.None;
        }
        private void HandleStartButtonClicked(ClickEvent clickEvent) {
            StartNewGame();
        }

        private void StartNewGame() {
            PlayerPrefs.DeleteAll();
            SceneTransition.Initiate(Constants.Scenes.ISLAND);
        }
    }
}