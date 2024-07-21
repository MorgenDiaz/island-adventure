
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character.Enemy {

    public class EnemyCombat : MonoBehaviour, INPCCombat {
        private Animator animatorComponent;
        private AnimationEventBubbler animationEventBubbler;
        private float _damage = 0f;
        public float Damage {
            get { return _damage; }
            set { _damage = value; }
        }
        private bool isAttacking = false;

        private readonly CloseRangeForwardDamageDealer damageDealer = new();
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
            if (isAttacking) return;
            animatorComponent.SetTrigger(Constants.AnimatorParams.ATTACK);
        }

        private void OnStartAttack() {
            isAttacking = true;
        }

        private void OnAttackHit() {
            damageDealer.TryToDealDamage(transform, Damage);
        }

        private void OnCompleteAttack() {
            isAttacking = false;
        }

        public void CancelAttack() {
            animatorComponent.ResetTrigger(Constants.AnimatorParams.ATTACK);
        }
    }
}