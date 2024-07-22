using RPG.Utility;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Character {
    public class Health : MonoBehaviour {
        private Animator animatorComponent;
        private AnimationEventBubbler animationEventBubbler;
        private float _healthPoints = 0f;

        public UnityAction OnDefeated = () => { };

        public float HealthPoints {
            get { return _healthPoints; }
            set {
                _healthPoints = Mathf.Max(0, value); ;
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

            if (HealthPoints == 0) {
                IsDefeated = true;
                animatorComponent.SetTrigger(Constants.AnimatorParams.DEFEATED);
                OnDefeated.Invoke();
            }

        }

        private void OnDefeatComplete() {
            Destroy(gameObject);
        }
    }
}