using RPG.Character;
using UnityEngine;

namespace RPG.Core {

    public class TriggerVictoryOnDeath : MonoBehaviour {
        [field: SerializeField] public Health HealthComponent { get; private set; }
        private void Awake() {
            HealthComponent = GetComponent<Health>();

            if (HealthComponent == null) {
                Debug.LogWarning($"{name} requires a Health component");
            }
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