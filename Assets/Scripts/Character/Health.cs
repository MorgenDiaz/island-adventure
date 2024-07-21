using System;
using UnityEngine;

namespace RPG.Character {
    public class Health : MonoBehaviour {
        private float _healthPoints = 0f;

        public float HealthPoints {
            get { return _healthPoints; }
            set {
                _healthPoints = Mathf.Max(0, value); ;
            }
        }

        public void TakeDamage(float damage) {
            HealthPoints -= damage;
        }
    }
}