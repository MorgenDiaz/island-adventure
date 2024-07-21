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

        private void Awake() {
            animatorComponent = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(float damage) {
            HealthPoints -= damage;

            if (HealthPoints == 0) {
                animatorComponent.SetTrigger(Constants.AnimatorParams.DEFEATED);
            }

        }
    }
}