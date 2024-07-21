using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character.Player {
    public class PlayerCombat : MonoBehaviour, ICombat {
        private Animator animatorComponent;
        private AnimationEventBubbler animationEventBubbler;
        private float _damage = 0f;

        private bool isAttacking = false;
        public float Damage {
            get { return _damage; }
            set { _damage = value; }
        }
        private readonly CloseRangeForwardDamageDealer damageDealer = new();

        private void Awake() {
            animatorComponent = GetComponentInChildren<Animator>();
            animationEventBubbler = GetComponentInChildren<AnimationEventBubbler>();
        }

        private void OnEnable() {
            animationEventBubbler.OnBubbleStartAttack += OnStartAttack;
            animationEventBubbler.OnBubbleAttackHit += OnAttackHit;
            animationEventBubbler.OnBubbleCompleteAttack += OnCompleteAtack;
        }

        private void OnDisable() {
            animationEventBubbler.OnBubbleStartAttack -= OnStartAttack;
            animationEventBubbler.OnBubbleAttackHit -= OnAttackHit;
            animationEventBubbler.OnBubbleCompleteAttack -= OnCompleteAtack;
        }

        public void HandleAttack(InputAction.CallbackContext context) {
            if (!context.performed) return;
            if (isAttacking) return;
            animatorComponent.SetTrigger(Constants.AnimatorParams.ATTACK);
        }

        private void OnStartAttack() {
            isAttacking = true;
        }

        private void OnAttackHit() {
            damageDealer.TryToDealDamage(transform, Damage);
        }

        private void OnCompleteAtack() {
            isAttacking = false;
        }

    }

}