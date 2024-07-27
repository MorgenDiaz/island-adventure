using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
namespace RPG.UI {

    public class UIController : MonoBehaviour {
        private PlayerInput _playerInputComponent;
        public PlayerInput PlayerInputComponent {
            get { return _playerInputComponent; }
            private set { _playerInputComponent = value; }
        }
        private IUIState currentState;
        private UIMainMenuState mainMenuState;
        private UIDialogueState dialogueState;
        private UIQuestItemState questItemState;
        private UIDocument documentComponent;
        public VisualElement DocumentRoot;
        public VisualElement MainMenuContainer;
        public VisualElement PlayerInfoContainer;
        public VisualElement DialogueContainer;
        public VisualElement QuestItemContainer;
        public List<Button> Buttons;
        public int CurrentSelection = 0;

        public Label healthText;
        public Label potionText;
        private void Awake() {
            GameObject gameManager = GameObject.FindWithTag(Constants.Tags.GAME_MANAGER);
            PlayerInputComponent = gameManager.GetComponent<PlayerInput>();

            mainMenuState = new(this);
            dialogueState = new(this);
            questItemState = new(this);

            documentComponent = GetComponent<UIDocument>();
            DocumentRoot = documentComponent.rootVisualElement;

            MainMenuContainer = DocumentRoot.Q<VisualElement>("main-menu-container");
            PlayerInfoContainer = DocumentRoot.Q<VisualElement>("player-info-container");
            DialogueContainer = DocumentRoot.Q<VisualElement>("dialogue-container");
            QuestItemContainer = DocumentRoot.Q<VisualElement>("quest-item-container");

            healthText = DocumentRoot.Q<Label>("health");
            potionText = DocumentRoot.Q<Label>("potions");
        }
        private void OnEnable() {
            EventManager.OnChangePlayerHealth += HandlePlayerHealthChange;
            EventManager.OnChangePotionCount += HandlePotionCountChange;
            EventManager.OnInitiateDialogue += HandleDialogueInitiation;
            EventManager.OnReceiveQuestItem += HandleReceiveQuestItem;
        }
        private void OnDisable() {
            EventManager.OnChangePlayerHealth -= HandlePlayerHealthChange;
            EventManager.OnChangePotionCount -= HandlePotionCountChange;
            EventManager.OnInitiateDialogue -= HandleDialogueInitiation;
            EventManager.OnReceiveQuestItem -= HandleReceiveQuestItem;
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
            currentState.SelectButton();
        }
        public void HandleNavigation(InputAction.CallbackContext context) {
            if (!context.performed || Buttons.Count == 0) return;

            Buttons[CurrentSelection].RemoveFromClassList("active");
            Vector2 input = context.ReadValue<Vector2>();

            CurrentSelection = Mathf.Clamp(CurrentSelection + (int)input.x, 0, Buttons.Count - 1);
            Buttons[CurrentSelection].AddToClassList("active");
        }
        private void HandlePlayerHealthChange(float health) {
            healthText.text = health.ToString();
        }
        private void HandlePotionCountChange(int potionCount) {
            potionText.text = potionCount.ToString();
        }

        private void HandleDialogueInitiation(TextAsset dialogue) {
            dialogueState.SetActiveDialogue(dialogue);
            SwitchState(dialogueState);
        }

        private void HandleReceiveQuestItem(QuestItemSO questItem) {
            questItemState.QuestItem = questItem;
            SwitchState(questItemState);
        }

        private void SwitchState(IUIState state) {

            currentState?.ExitState();
            currentState = state;
            currentState.EnterState();
        }
    }
}