using UnityEngine;

namespace RPG.Character {
    class AIPatrolState : IAIState {
        public void EnterState(IEnemyController enemy) {
            if (enemy is PatrollingEnemyController patrollingEnemy) {
                Debug.Log("Enered patrol state.");
                patrollingEnemy.PatrolComponent.OverrideSpeed(patrollingEnemy.Stats.walkSpeed);
                patrollingEnemy.PatrolComponent.ResetPatrol();
            }
            else {
                Debug.LogError("The patrol state is only compatible with patrolling enemies.");
            }
        }

        public void UpdateState(IEnemyController enemy) {
            if (enemy is PatrollingEnemyController patrollingEnemy) {
                Vector3 currentPosition = patrollingEnemy.transform.position;
                Vector3 nextPosition = patrollingEnemy.PatrolComponent.GetNextPosition();

                Vector3 movementDirection = nextPosition - currentPosition;

                patrollingEnemy.MovementComponent.MoveByOffset(movementDirection);

            }
            else {
                Debug.LogError("The patrol state is only compatible with patrolling enemies.");
            }
        }
    }
}