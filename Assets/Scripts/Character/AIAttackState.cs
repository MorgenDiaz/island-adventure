using UnityEngine;

namespace RPG.Character {
    class AIAttackState : IAIState {
        public void EnterState(EnemyController enemy) {
            enemy.movement.StopMovement();
            Debug.Log("Entering Attack State.");
        }

        public void UpdateState(EnemyController enemy) {
            Debug.Log("Attack!!!");
        }
    }
}