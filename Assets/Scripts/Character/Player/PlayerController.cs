using Newtonsoft.Json;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Character.Player {
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ICombat))]

    public class PlayerController : MonoBehaviour, ISaveable {
        private GameManager _gameManager;
        public CharacterStatsSO stats;
        private ControlledMovement _controlledMovement;
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

            _gameManager = FindObjectOfType<GameManager>();
            _controlledMovement = GetComponent<ControlledMovement>();
            HealthComponent = GetComponent<Health>();
            CombatComponent = GetComponent<ICombat>();
            _healthRestorationComponent = GetComponent<HealthRestoration>();

            EventManager.TriggerOnRegisterSaveable(this);
        }

        private void OnEnable() {
            HealthComponent.OnHealthChanged += HandleHealthChanged;
            HealthComponent.OnDefeated += HandlePlayerDefeated;
            EventManager.OnStartedCinematic += HandleCinematicStarted;
            EventManager.OnEndedCinematic += HandleCinematicEnded;
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
            HealthComponent.OnHealthChanged -= HandleHealthChanged;
            HealthComponent.OnDefeated -= HandlePlayerDefeated;
            EventManager.OnStartedCinematic -= HandleCinematicStarted;
            EventManager.OnEndedCinematic -= HandleCinematicEnded;
        }

        private void HandleHealthChanged(float health) {
            EventManager.TriggerChangePlayerHealth(health);
        }
        private void HandlePlayerDefeated() {
            EventManager.TriggerOnEndGame(false);
        }
        private void HandleCinematicStarted() {
            _controlledMovement.enabled = false;
        }

        private void HandleCinematicEnded() {
            _controlledMovement.enabled = true;
        }

        public void Save() {
            PlayerPrefs.SetFloat("health", HealthComponent.HealthPoints);
            PlayerPrefs.SetInt("potions", _healthRestorationComponent.PotionCount);
            PlayerPrefs.Save();
        }

        public void Load() {
            if (!_gameManager.HasSavedGameData()) return;

            HealthComponent.HealthPoints = PlayerPrefs.GetFloat("health");
            _healthRestorationComponent.PotionCount = PlayerPrefs.GetInt("potions");

            if (_gameManager.HasSavedSceneData()) {
                WarpPlayerToPortalSpawnPoint();
            }
        }

        private void WarpPlayerToPortalSpawnPoint() {
            NavMeshAgent playerAgent = GetComponent<NavMeshAgent>();

            PlayerSpawnPoint playerSpawnPoint = _gameManager.GetPlayerSpawnData();

            playerAgent.Warp(playerSpawnPoint.SpawnPosition);
            playerAgent.transform.rotation = playerSpawnPoint.SpawnRotation;
        }
    }
}