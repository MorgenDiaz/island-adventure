using UnityEngine;
namespace RPG.Character {
    public class AIReturnState : IAIState {
        public void EnterState(IEnemyController enemy) {
            enemy.MovementComponent.OverrideAgentSpeed(enemy.Stats.walkSpeed);
        }

        public void UpdateState(IEnemyController enemy) {
            if (enemy.MovementComponent.HasReachedDestination()) return;
            enemy.MovementComponent.MoveToDestination(enemy.OriginalPosition);
        }
    }
}