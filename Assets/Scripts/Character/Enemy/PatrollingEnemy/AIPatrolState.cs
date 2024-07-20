using UnityEngine;

namespace RPG.Character.Enemy {
    class AIPatrolState : IAIState {
        public void EnterState(IEnemyController enemy) {
            if (enemy is PatrollingEnemyController patrollingEnemy) {
                Debug.Log("Enered patrol state.");
                patrollingEnemy.MovementComponent.OverrideAgentSpeed(patrollingEnemy.Stats.walkSpeed);
                patrollingEnemy.PatrolComponent.ResetPatrol();
            }
            else {
                Debug.LogError("The patrol state is only compatible with patrolling enemies.");
            }
        }

        public void UpdateState(IEnemyController enemy) {
            if (enemy is PatrollingEnemyController patrollingEnemy) {
                Vector3 currentPosition = patrollingEnemy.transform.position;
                Vector3 nextPosition = patrollingEnemy.PatrolComponent.GetNextPosition(patrollingEnemy.Stats.walkSpeed);

                //normalize y
                currentPosition.y = 0;
                nextPosition.y = 0;

                if (nextPosition == currentPosition) return;

                Vector3 movementDirection = nextPosition - currentPosition;
                patrollingEnemy.MovementComponent.MoveByOffset(movementDirection);

            }
            else {
                Debug.LogError("The patrol state is only compatible with patrolling enemies.");
            }
        }
    }
}