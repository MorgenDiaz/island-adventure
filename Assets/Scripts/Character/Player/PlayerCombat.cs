using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character.Player {
    public class PlayerCombat : MonoBehaviour, ICombat {
        [SerializeField]
        private AudioClip _attackSound;
        private Animator animatorComponent;
        private AudioSource audioSourceComponent;
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
            audioSourceComponent = GetComponent<AudioSource>();
            animationEventBubbler = GetComponentInChildren<AnimationEventBubbler>();
        }

        private void OnEnable() {
            animationEventBubbler.OnBubbleStartAttack += HandleStartAttack;
            animationEventBubbler.OnBubbleAttackHit += HandleAttackHit;
            animationEventBubbler.OnBubbleCompleteAttack += HandleCompleteAtack;
        }

        private void OnDisable() {
            animationEventBubbler.OnBubbleStartAttack -= HandleStartAttack;
            animationEventBubbler.OnBubbleAttackHit -= HandleAttackHit;
            animationEventBubbler.OnBubbleCompleteAttack -= HandleCompleteAtack;
        }

        public void HandleAttack(InputAction.CallbackContext context) {
            if (!context.performed) return;
            if (isAttacking) return;
            animatorComponent.SetTrigger(Constants.AnimatorParams.ATTACK);
        }

        private void HandleStartAttack() {
            isAttacking = true;
            audioSourceComponent.clip = _attackSound;
            audioSourceComponent.Play();
        }

        private void HandleAttackHit() {
            damageDealer.TryToDealDamage(transform, Damage);
        }

        private void HandleCompleteAtack() {
            isAttacking = false;
        }

    }

}