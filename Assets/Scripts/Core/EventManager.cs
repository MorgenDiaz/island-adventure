
using System;
using Ink.Runtime;
using RPG.Item;
using UnityEngine.Events;
namespace RPG.Core {

    public static class EventManager {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePotionCount;
        public static event UnityAction<Story, Func<object>> OnInitiateDialogue;
        public static event UnityAction<IItem> OnReceiveQuestItem;

        public static void TriggerChangePlayerHealth(float health) => OnChangePlayerHealth?.Invoke(health);
        public static void TriggerChangePotionCount(int potionCount) => OnChangePotionCount?.Invoke(potionCount);
        public static void TriggerInitiateDialogue(Story story, Func<object> VerifyQuestRequirementsFNC) => OnInitiateDialogue?.Invoke(story, VerifyQuestRequirementsFNC);
        public static void TriggerReceiveQuestItem(IItem questItem) => OnReceiveQuestItem?.Invoke(questItem);
    }
}