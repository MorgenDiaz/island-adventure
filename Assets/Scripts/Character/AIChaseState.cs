using UnityEngine;

namespace RPG.Character {
    class AIChaseState : IAIState {
        public void EnterState(EnemyController enemy) {
            Debug.Log("Entered chase state.");

        }

        public void UpdateState(EnemyController enemy) {
            Movement movement = enemy.movement;
            GameObject player = enemy.player;
            movement.MoveToDestination(player.transform.position);
        }


    }
}