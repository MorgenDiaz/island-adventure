using UnityEngine;
namespace RPG.Character.NPC {
    public class AIReturnState : IAIState {
        private readonly IEnemyController _enemy;

        public AIReturnState(IEnemyController enemy) {
            _enemy = enemy;
        }
        public void EnterState() {
            _enemy.Components.MovementComponent.OverrideAgentSpeed(_enemy.Stats.walkSpeed);
        }

        public void UpdateState() {
            if (_enemy.Components.MovementComponent.HasReachedDestination()) return;
            _enemy.Components.MovementComponent.MoveToDestination(_enemy.OriginalPosition);
        }

        public void ExitState() {
        }
    }
}