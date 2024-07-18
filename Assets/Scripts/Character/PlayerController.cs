using UnityEngine;

namespace RPG.Character {
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Combat))]
    public class PlayerController : MonoBehaviour {
        public CharacterStatsSO stats;

        private Health _health;

        public Health HealthComponent {
            get { return _health; }
            private set { _health = value; }
        }

        private Combat _combat;

        public Combat CombatComponent {
            get { return _combat; }
            private set { _combat = value; }
        }
        private void Awake() {
            if (stats == null) {
                Debug.LogWarning($"{name} does not have character stats.");
            }
            HealthComponent = GetComponent<Health>();
            CombatComponent = GetComponent<Combat>();
        }
    }
}