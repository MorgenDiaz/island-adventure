using UnityEngine;

namespace RPG.Character {
    class AIAttackState : AIBaseState {
        public override void EnterState(EnemyController enemy) {
            enemy.movement.StopMovement();
            Debug.Log("Entering Attack State.");
        }

        public override void UpdateState(EnemyController enemy) {
            Debug.Log("Attack!!!");
        }
    }
}