using UnityEngine;

namespace RPG.Character {
    class AIPatrolState : IAIState {
        public void EnterState(IEnemyController enemy) {
            if (enemy is PatrollingEnemyController patrollingEnemy) {
                Debug.Log("Enered patrol state.");
            }
            else {
                Debug.LogError("The patrol state is only compatible with patrolling enemies.");
            }
        }

        public void UpdateState(IEnemyController enemy) {
            if (enemy is PatrollingEnemyController patrollingEnemy) {
                Debug.Log("Enered patrol state.");
            }
            else {
                Debug.LogError("The patrol state is only compatible with patrolling enemies.");
            }
        }
    }
}