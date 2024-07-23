using System;
using UnityEngine;
namespace RPG.Character.Enemy {
    public class AIDefeatedState : IAIState {
        private readonly IEnemyController _enemy;
        public AIDefeatedState(IEnemyController enemy) {
            _enemy = enemy;
        }
        public void EnterState() {

        }

        public void UpdateState() {
            Debug.Log("defeated!");
        }
        public void ExitState() {

        }
    }
}