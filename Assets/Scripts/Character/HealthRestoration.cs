using RPG.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character {
    class HealthRestoration : MonoBehaviour {
        private Health healthComponent;
        [SerializeField]
        private int _potionCount = 0;

        public int PotionCount {
            get { return _potionCount; }
            set { _potionCount = value; }
        }
        [SerializeField]
        private int _potionHealAmount = 0;
        private void Awake() {
            healthComponent = GetComponent<Health>();
        }

        public void HandleHeal(InputAction.CallbackContext context) {
            if (!context.performed || healthComponent.IsHealthFull() || _potionCount == 0) return;
            healthComponent.Heal(_potionHealAmount);
            _potionCount--;

            EventManager.TriggerChangePotionCount(_potionCount);
        }
    }
}