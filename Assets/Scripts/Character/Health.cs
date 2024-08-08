using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Character {
    public class Health : MonoBehaviour {
        private Animator animatorComponent;
        private AnimationEventBubbler animationEventBubbler;

        public UnityAction OnDefeated = () => { };

        public event UnityAction<float> OnHealthChanged;

        private float _maxHealthPoints = 0f;
        public float MaxHealthPoints {
            get { return _maxHealthPoints; }
            set {
                _maxHealthPoints = value;
            }
        }
        private float _healthPoints = 0f;

        public float HealthPoints {
            get { return _healthPoints; }
            set {
                _healthPoints = Mathf.Clamp(value, 0, MaxHealthPoints); ;
            }
        }

        private bool _isDefeated = false;

        public bool IsDefeated {
            get { return _isDefeated; }
            private set { _isDefeated = value; }
        }

        private void Awake() {
            animatorComponent = GetComponentInChildren<Animator>();
            animationEventBubbler = GetComponentInChildren<AnimationEventBubbler>();
        }

        private void OnEnable() {
            animationEventBubbler.OnBubbleDefeated += OnDefeatComplete;
        }

        private void OnDisable() {
            animationEventBubbler.OnBubbleDefeated -= OnDefeatComplete;
        }

        public void TakeDamage(float damage) {
            if (IsDefeated) return;

            HealthPoints -= damage;
            OnHealthChanged?.Invoke(HealthPoints);

            if (HealthPoints == 0) {
                IsDefeated = true;
                animatorComponent.SetTrigger(Constants.AnimatorParams.DEFEATED);
            }

        }
        public void Heal(float amount) {
            HealthPoints += amount;
            OnHealthChanged?.Invoke(HealthPoints);
        }

        public bool IsHealthFull() {
            return HealthPoints == MaxHealthPoints;
        }
        private void OnDefeatComplete() {
            OnDefeated.Invoke();
            Destroy(gameObject);
        }
    }
}