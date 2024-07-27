using System.Collections.Generic;
using Ink.Runtime;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI {
    public class UIDialogueState : IUIState {
        private readonly UIController _controller;

        public UIDialogueState(UIController uiController) {
            _controller = uiController;
        }
        private Label _dialogueText;
        private Button _nextButton;
        private VisualElement _choiceGroup;

        private List<Button> _linearDialogueButtons;
        private List<Button> _choiceDialogueButtons;

        private Story _currentStory;

        public void EnterState() {
            _dialogueText = _controller.DialogueContainer.Q<Label>("dialogue-text");
            _nextButton = _controller.DialogueContainer.Q<Button>("dialogue-next-button");
            _choiceGroup = _controller.DialogueContainer.Q<VisualElement>("choice-group");

            _linearDialogueButtons = new List<Button> { _nextButton };
            _choiceDialogueButtons = _choiceGroup.Query<Button>(null, "dialogue-button").ToList();

            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.UI);

            ProgressDialogue();

            _controller.DialogueContainer.style.display = DisplayStyle.Flex;
        }
        public void SelectButton() {
            if (_currentStory.currentChoices != null && _currentStory.currentChoices.Count > 0) {
                _currentStory.ChooseChoiceIndex(_controller.CurrentSelection);
                //Skip logging players choice to main dialogue window.
                _currentStory.Continue();

                _controller.Buttons[_controller.CurrentSelection].RemoveFromClassList("active");
                _controller.CurrentSelection = 0;
            }

            ProgressDialogue();

        }
        public void ExitState() {
            _controller.DialogueContainer.style.display = DisplayStyle.None;
            _controller.PlayerInputComponent.SwitchCurrentActionMap(Constants.ActionMaps.GAMEPLAY);
        }
        public void SetActiveDialogue(TextAsset dialogue) {
            _currentStory = new Story(dialogue.text);
        }
        private void ProgressDialogue() {
            if (_currentStory.canContinue) {
                _dialogueText.text = _currentStory.Continue();
                DisplayNextButton();
            }
            else {
                ExitState();
            }

            if (_currentStory.currentChoices == null || _currentStory.currentChoices.Count == 0) return;
            DisplayChoiceButtons();
        }

        private void DisplayNextButton() {
            _choiceGroup.style.display = DisplayStyle.None;
            _nextButton.style.display = DisplayStyle.Flex;
            _controller.Buttons = _linearDialogueButtons;
            _controller.Buttons[_controller.CurrentSelection].AddToClassList("active");
        }
        private void DisplayChoiceButtons() {
            _controller.Buttons = _choiceDialogueButtons;
            _nextButton.style.display = DisplayStyle.None;
            _choiceGroup.style.display = DisplayStyle.Flex;

            _choiceGroup.Clear();
            _choiceDialogueButtons.Clear();

            _currentStory.currentChoices.ForEach(CreateChoiceButton);
            _controller.Buttons[_controller.CurrentSelection].AddToClassList("active");

        }

        private void CreateChoiceButton(Choice choice) {
            Button button = new();
            button.AddToClassList("menu-button");
            button.AddToClassList("dialogue-button");

            button.text = choice.text;

            _choiceDialogueButtons.Add(button);
            _choiceGroup.Add(button);
        }
    }
}