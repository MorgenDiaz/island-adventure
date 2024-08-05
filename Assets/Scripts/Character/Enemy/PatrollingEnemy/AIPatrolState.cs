using UnityEngine;

namespace RPG.Character.Enemy {
    class AIPatrolState : IAIState {
        private readonly PatrollingEnemyController _enemy;

        public AIPatrolState(PatrollingEnemyController enemy) {
            _enemy = enemy;
        }
        public void EnterState() {
            _enemy.Components.MovementComponent.OverrideAgentSpeed(_enemy.Stats.walkSpeed);
            _enemy.PatrolComponent.ResetPatrol();
        }

        public void UpdateState() {
            Vector3 currentPosition = _enemy.transform.position;
            Vector3 nextPosition = _enemy.PatrolComponent.GetNextPosition(_enemy.Stats.walkSpeed);

            //normalize y
            currentPosition.y = 0;
            nextPosition.y = 0;

            if (nextPosition == currentPosition) return;

            Vector3 movementDirection = nextPosition - currentPosition;
            _enemy.Components.MovementComponent.MoveByOffset(movementDirection);

        }

        public void ExitState() {

        }
    }
}