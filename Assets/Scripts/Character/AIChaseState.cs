using UnityEngine;

namespace RPG.Character {
    class AIChaseState : IAIState {
        public void EnterState(IEnemyController enemy) {
            Debug.Log("Entered chase state.");

        }

        public void UpdateState(IEnemyController enemy) {
            Movement movement = enemy.MovementComponent;
            GameObject player = enemy.Player;
            movement.MoveToDestination(player.transform.position);
        }


    }
}