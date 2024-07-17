using UnityEngine;
namespace RPG.Character {
    public class AIReturnState : AIBaseState {
        public override void EnterState(EnemyController enemy) {
            Debug.Log("Entering return state");
        }


        public override void UpdateState(EnemyController enemy) {
            enemy.movement.MoveToDestination(enemy.originalPosition);
        }
    }
}