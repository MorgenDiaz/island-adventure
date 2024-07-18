using UnityEngine;

namespace RPG.Character {
    class AIAttackState : IAIState {
        public void EnterState(IEnemyController enemy) {
            enemy.MovementComponent.StopMovement();
            Debug.Log("Entering Attack State.");
        }

        public void UpdateState(IEnemyController enemy) {
            Debug.Log("Attack!!!");
        }
    }
}