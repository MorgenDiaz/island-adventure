using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI {

    public class UIController : MonoBehaviour {
        private IUIState currentState;
        private UIMainMenuState mainMenuState;
        private UIDocument documentComponent;
        public VisualElement DocumentRoot;
        public List<Button> Buttons;

        public int CurrentSelection = 0;
        private void Awake() {
            mainMenuState = new(this);
            documentComponent = GetComponent<UIDocument>();
            DocumentRoot = documentComponent.rootVisualElement;
        }
        private void Start() {
            currentState = mainMenuState;
            currentState.EnterState();
        }

        public void HandleInteraction(InputAction.CallbackContext context) {
            if (!context.performed) return;
            print("did handle interaction!");
            print(context);
            currentState.SelectButton();
        }

        public void HandleNavigation(InputAction.CallbackContext context) {
            if (!context.performed || Buttons.Count == 0) return;

            Buttons[CurrentSelection].RemoveFromClassList("active");
            Vector2 input = context.ReadValue<Vector2>();

            CurrentSelection = Mathf.Clamp(CurrentSelection + (int)input.x, 0, Buttons.Count - 1);
            Buttons[CurrentSelection].AddToClassList("active");
        }
    }
}