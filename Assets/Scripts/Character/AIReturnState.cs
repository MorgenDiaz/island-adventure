using UnityEngine;
namespace RPG.Character {
    public class AIReturnState : IAIState {
        public void EnterState(IEnemyController enemy) {
            Debug.Log("Entering return state");
        }


        public void UpdateState(IEnemyController enemy) {
            enemy.MovementComponent.MoveToDestination(enemy.OriginalPosition);
        }
    }
}