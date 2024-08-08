using RPG.Character;
using UnityEngine;

namespace RPG.Core {

    public class TriggerVictoryOnDeath : MonoBehaviour {
        [SerializeField]
        private Health _healthComponent;
        public Health HealthComponent { get { return _healthComponent; } private set { _healthComponent = value; } }
        private void Awake() {
            Debug.LogWarning($"{name} requires a Health component");
        }
        private void OnEnable() {
            HealthComponent.OnDefeated += HandleCharacterDefeated;
        }
        private void OnDisable() {
            HealthComponent.OnDefeated -= HandleCharacterDefeated;
        }

        private void HandleCharacterDefeated() {
            EventManager.TriggerOnEndGame(true);
        }

    }
}