
using System;
using Ink.Runtime;
using RPG.Item;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Core {

    public static class EventManager {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePotionCount;
        public static event UnityAction<Story, Func<object>> OnInitiateDialogue;
        public static event UnityAction<IItem> OnReceiveQuestItem;
        public static event UnityAction<string> OnEquipItemRightHand;
        public static event UnityAction OnUnEquipItemRightHand;
        public static event UnityAction<Collider, int, Transform> OnEnterPortal;
        public static event UnityAction<ISaveable> OnRegisterSaveableObject;
        public static event UnityAction<string> OnKillEnemy;
        public static event UnityAction OnStartedCinematic;
        public static event UnityAction OnEndedCinematic;

        public static void TriggerChangePlayerHealth(float health) => OnChangePlayerHealth?.Invoke(health);
        public static void TriggerChangePotionCount(int potionCount) => OnChangePotionCount?.Invoke(potionCount);
        public static void TriggerInitiateDialogue(Story story, Func<object> VerifyQuestRequirementsFNC) => OnInitiateDialogue?.Invoke(story, VerifyQuestRequirementsFNC);
        public static void TriggerReceiveQuestItem(IItem questItem) => OnReceiveQuestItem?.Invoke(questItem);
        public static void TriggerEquipItemRightHand(string itemTag) => OnEquipItemRightHand?.Invoke(itemTag);
        public static void TriggerUnEquipItemRightHand() => OnUnEquipItemRightHand?.Invoke();
        public static void TriggerOnEnterPortal(Collider playerCollider, int sceneIndex, Transform portalSpawnPoint) => OnEnterPortal?.Invoke(playerCollider, sceneIndex, portalSpawnPoint);
        public static void TriggerOnRegisterSaveable(ISaveable saveable) => OnRegisterSaveableObject?.Invoke(saveable);
        public static void TriggerOnKillEnemy(string enemyId) => OnKillEnemy?.Invoke(enemyId);
        public static void TriggerOnStartedCinematic() => OnStartedCinematic?.Invoke();
        public static void TriggerOnEndedCinematic() => OnEndedCinematic?.Invoke();

    }
}