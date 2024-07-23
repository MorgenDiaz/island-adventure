
using RPG.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIMainMenuState : IUIState {
        private readonly UIController _controller;
        public UIMainMenuState(UIController controller) {
            _controller = controller;
        }
        public void EnterState() {
            _controller.Buttons = _controller.DocumentRoot.Query<Button>(null, "menu-button").ToList();
            Button startButton = _controller.DocumentRoot.Query<Button>("StartButton");
            startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);
            _controller.Buttons[0].AddToClassList("active");
            Debug.Log(string.Join(", ", _controller.Buttons[0].GetClasses()));
        }
        public void SelectButton() {
            Button button = _controller.Buttons[_controller.CurrentSelection];
            if (button.name == "StartButton") {
                SceneTransition.initiate(1);
            }
        }

        private void OnStartButtonClicked(ClickEvent clickEvent) {
            SceneTransition.initiate(1);
        }
    }
}