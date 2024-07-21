using UnityEngine;

namespace RPG.Character.Enemy {
    class AIAttackState : IAIState {
        public void EnterState(IEnemyController enemy) {
            enemy.MovementComponent.StopMovement();
        }

        public void UpdateState(IEnemyController enemy) {
            enemy.CombatComponent.Attack();
        }
    }
}