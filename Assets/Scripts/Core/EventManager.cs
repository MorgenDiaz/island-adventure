using RPG.Quest;

namespace RPG.Core {
    using UnityEngine;
    using UnityEngine.Events;

    public static class EventManager {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePotionCount;
        public static event UnityAction<TextAsset> OnInitiateDialogue;
        public static event UnityAction<QuestItemSO> OnReceiveQuestItem;

        public static void TriggerChangePlayerHealth(float health) => OnChangePlayerHealth?.Invoke(health);
        public static void TriggerChangePotionCount(int potionCount) => OnChangePotionCount?.Invoke(potionCount);
        public static void TriggerInitiateDialogue(TextAsset dialogue) => OnInitiateDialogue?.Invoke(dialogue);
        public static void TriggerReceiveQuestItem(QuestItemSO questItem) => OnReceiveQuestItem?.Invoke(questItem);
    }
}