namespace RPG.Core {
    using UnityEngine.Events;

    public static class EventManager {
        public static event UnityAction<float> OnChangePlayerHealth;

        public static void TriggerChangePlayerHealth(float health) => OnChangePlayerHealth?.Invoke(health);
    }
}