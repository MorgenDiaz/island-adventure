
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character {

    public class EnemyCombat : MonoBehaviour, ICombat {

        private float _damage = 0f;
        public float Damage {
            get { return _damage; }
            set { _damage = value; }
        }

        private Animator animatorComponent;
        protected void OnAwake() {
            animatorComponent = GetComponentInChildren<Animator>();
        }

        public void HandleAttack(InputAction.CallbackContext context) {
            if (!context.performed) return;

            animatorComponent.SetTrigger(Constants.AnimatorParams.ATTACK);
        }
    }
}