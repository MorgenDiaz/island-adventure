using UnityEngine;
using UnityEngine.UI;

namespace RPG.Character {
    public class NPCHealthBar : MonoBehaviour {
        private Health healthComponent;
        private Slider sliderComponent;

        private float _maxHealth;

        public float MaxHealth {
            get { return _maxHealth; }
            set {
                _maxHealth = value;
                sliderComponent.maxValue = _maxHealth;
            }
        }
        private float _health;

        public float Health {
            get { return _health; }
            set {
                _health = value;
                sliderComponent.value = _health;
            }
        }
        private void Awake() {
            healthComponent = GetComponent<Health>();
            sliderComponent = GetComponentInChildren<Slider>();
        }

        private void OnEnable() {
            healthComponent.OnHealthChanged += HandleHealthChange;
        }

        private void OnDisable() {
            healthComponent.OnHealthChanged -= HandleHealthChange;
        }

        public void HandleHealthChange(float health) {
            sliderComponent.value = health;
        }
    }
}