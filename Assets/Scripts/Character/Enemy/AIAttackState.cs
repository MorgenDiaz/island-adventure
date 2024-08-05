using UnityEngine;

namespace RPG.Character.Enemy {
    class AIAttackState : IAIState {
        private readonly IEnemyController _enemy;

        public AIAttackState(IEnemyController enemy) {
            _enemy = enemy;
        }

        public void EnterState() {
            _enemy.Components.MovementComponent.StopMovement();
        }

        public void UpdateState() {
            if (_enemy.Components.Player == null) {
                _enemy.Components.CombatComponent.CancelAttack();
                return;
            }

            _enemy.transform.LookAt(_enemy.Components.Player.transform);
            _enemy.Components.CombatComponent.Attack();
        }

        public void ExitState() {
            _enemy.Components.CombatComponent.CancelAttack();
        }
    }
}