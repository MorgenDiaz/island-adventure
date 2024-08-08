using System;
using System.Collections.Generic;
using Ink.Runtime;
using RPG.Character.Player;
using RPG.Core;
using RPG.Item;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
namespace RPG.UI {

    public class UIController : MonoBehaviour {
        private PlayerInput _playerInputComponent;
        public Inventory InventoryComponent;
        public Equipment EquipmentComponent;
        public PlayerInput PlayerInputComponent {
            get { return _playerInputComponent; }
            private set { _playerInputComponent = value; }
        }
        public AudioSource AudioSourceComponent { get; private set; }
        [SerializeField]
        private AudioClip _rewardSound;
        public AudioClip RewardSound {
            get { return _rewardSound; }
            private set { _rewardSound = value; }
        }
        [SerializeField]
        private AudioClip _victorySound;
        public AudioClip VictorySound {
            get { return _victorySound; }
            private set { _victorySound = value; }
        }

        [SerializeField]
        private AudioClip _gameOverSound;
        public AudioClip GameOverSound {
            get { return _gameOverSound; }
            private set { _gameOverSound = value; }
        }

        private IUIState currentState;
        private UIMainMenuState mainMenuState;
        private UIDialogueState dialogueState;
        private UIQuestItemState questItemState;
        private UIInventoryState inventoryState;
        private UIVictoryState victoryState;
        private UIGameOverState gameOverState;
        private UIDocument documentComponent;
        public VisualElement DocumentRoot;
        public VisualElement MainMenuContainer;
        public VisualElement PlayerInfoContainer;
        public VisualElement DialogueContainer;
        public VisualElement QuestItemContainer;
        public VisualElement InventoryContainer;
        public VisualElement VictoryContainer;
        public VisualElement GameOverContainer;
        public List<Button> Buttons = new();
        public int CurrentSelection = 0;

        public Label healthText;
        public Label potionText;
        private void Awake() {
            GameObject gameManager = GameObject.FindWithTag(Constants.Tags.GAME_MANAGER);
            PlayerInputComponent = gameManager.GetComponent<PlayerInput>();
            AudioSourceComponent = GetComponent<AudioSource>();

            mainMenuState = new(this);
            dialogueState = new(this);
            questItemState = new(this);
            inventoryState = new(this);
            victoryState = new(this);
            gameOverState = new(this);

            documentComponent = GetComponent<UIDocument>();
            DocumentRoot = documentComponent.rootVisualElement;

            MainMenuContainer = DocumentRoot.Q<VisualElement>("main-menu-container");
            PlayerInfoContainer = DocumentRoot.Q<VisualElement>("player-info-container");
            DialogueContainer = DocumentRoot.Q<VisualElement>("dialogue-container");
            QuestItemContainer = DocumentRoot.Q<VisualElement>("quest-item-container");
            InventoryContainer = DocumentRoot.Q<VisualElement>("inventory-container");
            VictoryContainer = DocumentRoot.Q("victory-container");
            GameOverContainer = DocumentRoot.Q("game-over-container");

            healthText = DocumentRoot.Q<Label>("health");
            potionText = DocumentRoot.Q<Label>("potions");
        }
        private void OnEnable() {
            EventManager.OnChangePlayerHealth += HandlePlayerHealthChange;
            EventManager.OnChangePotionCount += HandlePotionCountChange;
            EventManager.OnInitiateDialogue += HandleDialogueInitiation;
            EventManager.OnReceiveQuestItem += HandleReceiveQuestItem;
            EventManager.OnEndGame += HandleGameOver;
        }
        private void OnDisable() {
            EventManager.OnChangePlayerHealth -= HandlePlayerHealthChange;
            EventManager.OnChangePotionCount -= HandlePotionCountChange;
            EventManager.OnInitiateDialogue -= HandleDialogueInitiation;
            EventManager.OnReceiveQuestItem -= HandleReceiveQuestItem;
            EventManager.OnEndGame -= HandleGameOver;
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
        public void HandleInventoryOpened(InputAction.CallbackContext context) {
            if (!context.performed) return;

            SwitchState(inventoryState);
        }
        private void HandlePlayerHealthChange(float health) {
            healthText.text = health.ToString();
        }
        private void HandlePotionCountChange(int potionCount) {
            potionText.text = potionCount.ToString();
        }
        private void HandleDialogueInitiation(Story story, Func<object> VerifyQuestRequirementsFNC) {
            dialogueState.PrepareDialogue(story, VerifyQuestRequirementsFNC);
            SwitchState(dialogueState);
        }
        private void HandleReceiveQuestItem(IItem questItem) {
            questItemState.QuestItem = questItem;
            SwitchState(questItemState);
        }

        private void HandleGameOver(bool isVictory) {
            if (isVictory) {
                SwitchState(victoryState);
            }
            else {
                SwitchState(gameOverState);
            }
        }

        private void SwitchState(IUIState state) {
            currentState?.ExitState();
            currentState = state;
            currentState.EnterState();
        }
    }
}