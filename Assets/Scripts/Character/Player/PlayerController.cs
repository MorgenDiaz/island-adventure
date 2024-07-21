using UnityEngine;

namespace RPG.Character.Player {
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ICombat))]

    public class PlayerController : MonoBehaviour {
        public CharacterStatsSO stats;

        private Health _health;

        public Health HealthComponent {
            get { return _health; }
            private set { _health = value; }
        }

        private ICombat _combat;

        public ICombat CombatComponent {
            get { return _combat; }
            private set { _combat = value; }
        }
        private void Awake() {
            if (stats == null) {
                Debug.LogWarning($"{name} does not have character stats.");
            }

            HealthComponent = GetComponent<Health>();
            CombatComponent = GetComponent<ICombat>();

            HealthComponent.HealthPoints = stats.health;
            CombatComponent.Damage = stats.damage;
        }
    }
}