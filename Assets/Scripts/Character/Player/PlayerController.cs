using RPG.Core;
using UnityEngine;
namespace RPG.Character.Player {
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ICombat))]

    public class PlayerController : MonoBehaviour, ISaveable {
        public CharacterStatsSO stats;

        private Health _health;

        public Health HealthComponent {
            get { return _health; }
            private set { _health = value; }
        }
        private HealthRestoration _healthRestorationComponent;

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
            _healthRestorationComponent = GetComponent<HealthRestoration>();
            EventManager.TriggerOnRegisterSaveable(this);
        }

        private void OnEnable() {
            HealthComponent.OnHealthChanged += OnHealthChanged;
        }

        private void Start() {
            HealthComponent.MaxHealthPoints = stats.health;
            HealthComponent.HealthPoints = stats.health;
            CombatComponent.Damage = stats.damage;

            Load();

            EventManager.TriggerChangePlayerHealth(HealthComponent.HealthPoints);
            EventManager.TriggerChangePotionCount(_healthRestorationComponent.PotionCount);
        }
        private void OnDisable() {
            HealthComponent.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float health) {
            EventManager.TriggerChangePlayerHealth(health);
        }

        public void Save() {
            PlayerPrefs.SetFloat("health", HealthComponent.HealthPoints);
            PlayerPrefs.SetInt("potions", _healthRestorationComponent.PotionCount);
            PlayerPrefs.Save();
        }

        public void Load() {
            if (!PlayerPrefs.HasKey("health")) return;
            HealthComponent.HealthPoints = PlayerPrefs.GetFloat("health");
            _healthRestorationComponent.PotionCount = PlayerPrefs.GetInt("potions");
        }
    }
}