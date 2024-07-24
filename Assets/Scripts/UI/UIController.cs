using System.Collections.Generic;
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
namespace RPG.UI {

    public class UIController : MonoBehaviour {
        private IUIState currentState;
        private UIMainMenuState mainMenuState;
        private UIDocument documentComponent;
        public VisualElement DocumentRoot;
        public VisualElement MainMenuContainer;
        public VisualElement PlayerInfoContainer;
        public List<Button> Buttons;
        public int CurrentSelection = 0;

        public Label healthText;
        public Label potionText;
        private void Awake() {
            mainMenuState = new(this);
            documentComponent = GetComponent<UIDocument>();
            DocumentRoot = documentComponent.rootVisualElement;
            MainMenuContainer = DocumentRoot.Q<VisualElement>("main-menu-container");
            PlayerInfoContainer = DocumentRoot.Q<VisualElement>("player-info-container");
            healthText = DocumentRoot.Q<Label>("health");
            potionText = DocumentRoot.Q<Label>("potions");
        }
        private void OnEnable() {
            EventManager.OnChangePlayerHealth += HandlePlayerHealthChange;
            EventManager.OnChangePotionCount += HandlePotionCountChange;
        }
        private void OnDisable() {
            EventManager.OnChangePlayerHealth -= HandlePlayerHealthChange;
            EventManager.OnChangePotionCount -= HandlePotionCountChange;
        }
        private void Start() {
            int activeScene = SceneTransition.GetActiveSceneId();

            if (activeScene == Constants.Scenes.MAIN_MENU) {
                currentState = mainMenuState;
                currentState.EnterState();
            }
            else {
                PlayerInfoContainer.style.display = DisplayStyle.Flex;
            }

        }

        public void HandleInteraction(InputAction.CallbackContext context) {
            if (!context.performed) return;
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

        public void HandlePlayerHealthChange(float health) {
            healthText.text = health.ToString();
        }

        public void HandlePotionCountChange(int potionCount) {
            potionText.text = potionCount.ToString();
        }
    }
}