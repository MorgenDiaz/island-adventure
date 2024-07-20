using UnityEngine;

namespace RPG.Character {
    class AIAttackState : IAIState {
        public void EnterState(IEnemyController enemy) {
            enemy.MovementComponent.StopMovement();

        }

        public void UpdateState(IEnemyController enemy) {
            Debug.Log("Attack!!!");
            enemy.CombatComponent.Attack();
        }
    }
}