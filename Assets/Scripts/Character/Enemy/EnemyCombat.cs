
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character.Enemy {

    public class EnemyCombat : MonoBehaviour, INPCCombat {
        private float _damage = 0f;
        public float Damage {
            get { return _damage; }
            set { _damage = value; }
        }

        private Animator animatorComponent;
        private AnimationEventBubbler animationEventBubbler;

        protected void Awake() {
            animatorComponent = GetComponentInChildren<Animator>();
            animationEventBubbler = GetComponentInChildren<AnimationEventBubbler>();
        }

        protected void OnEnable() {
            animationEventBubbler.OnBubbleStartAttack += OnStartAttack;
            animationEventBubbler.OnBubbleAttackHit += OnAttackHit;
            animationEventBubbler.OnBubbleCompleteAttack += OnCompleteAttack;
        }

        protected void OnDisable() {
            animationEventBubbler.OnBubbleStartAttack -= OnStartAttack;
            animationEventBubbler.OnBubbleAttackHit -= OnAttackHit;
            animationEventBubbler.OnBubbleCompleteAttack -= OnCompleteAttack;
        }
        public void Attack() {
            animatorComponent.SetTrigger(Constants.AnimatorParams.ATTACK);
        }

        private void OnStartAttack() {

        }

        private void OnAttackHit() {
        }

        private void OnCompleteAttack() {

        }

    }
}