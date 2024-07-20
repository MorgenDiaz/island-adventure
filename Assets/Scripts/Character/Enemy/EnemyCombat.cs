
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

        protected void Awake() {
            animatorComponent = GetComponentInChildren<Animator>();
        }

        public void Attack() {
            animatorComponent.SetTrigger(Constants.AnimatorParams.ATTACK);
        }

    }
}