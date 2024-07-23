using UnityEngine;

namespace RPG.Character.Enemy {
    class AIChaseState : IAIState {
        private readonly IEnemyController _enemy;
        public AIChaseState(IEnemyController enemy) {
            _enemy = enemy;
        }
        public void EnterState() {
            _enemy.MovementComponent.OverrideAgentSpeed(_enemy.Stats.runSpeed);
        }

        public void UpdateState() {

            Movement movement = _enemy.MovementComponent;
            GameObject player = _enemy.Player;

            movement.MoveToDestination(player.transform.position);
        }

        public void ExitState() {

        }
    }
}