using System;
using RPG.Utility;
using UnityEngine;
namespace RPG.Character {
    public class Health : MonoBehaviour {
        private Animator animatorComponent;
        private float _healthPoints = 0f;

        public float HealthPoints {
            get { return _healthPoints; }
            set {
                _healthPoints = Mathf.Max(0, value); ;
            }
        }

        private bool isDefeated = false;

        private void Awake() {
            animatorComponent = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(float damage) {
            if (isDefeated) return;

            HealthPoints -= damage;

            if (HealthPoints == 0) {
                isDefeated = true;
                animatorComponent.SetTrigger(Constants.AnimatorParams.DEFEATED);
            }

        }
    }
}