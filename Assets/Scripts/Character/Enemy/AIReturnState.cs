using UnityEngine;
namespace RPG.Character.Enemy {
    public class AIReturnState : IAIState {
        private readonly IEnemyController _enemy;

        public AIReturnState(IEnemyController enemy) {
            _enemy = enemy;
        }
        public void EnterState() {
            _enemy.MovementComponent.OverrideAgentSpeed(_enemy.Stats.walkSpeed);
        }

        public void UpdateState() {
            if (_enemy.MovementComponent.HasReachedDestination()) return;
            _enemy.MovementComponent.MoveToDestination(_enemy.OriginalPosition);
        }

        public void ExitState() {
        }
    }
}