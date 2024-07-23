using UnityEngine;

namespace RPG.Character.Enemy {
    class AIAttackState : IAIState {
        private readonly IEnemyController _enemy;

        public AIAttackState(IEnemyController enemy) {
            _enemy = enemy;
        }

        public void EnterState() {
            _enemy.MovementComponent.StopMovement();
        }

        public void UpdateState() {
            if (_enemy.Player == null) {
                _enemy.CombatComponent.CancelAttack();
                return;
            }

            _enemy.transform.LookAt(_enemy.Player.transform);
            _enemy.CombatComponent.Attack();
        }

        public void ExitState() {
            _enemy.CombatComponent.CancelAttack();
        }
    }
}