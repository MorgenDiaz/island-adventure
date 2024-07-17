using UnityEngine;
namespace RPG.Character {
    public class AIReturnState : IAIState {
        public void EnterState(EnemyController enemy) {
            Debug.Log("Entering return state");
        }


        public void UpdateState(EnemyController enemy) {
            enemy.movement.MoveToDestination(enemy.originalPosition);
        }
    }
}