using UnityEngine;

namespace RPG.Character {
    class AIChaseState : AIBaseState {
        public override void EnterState(EnemyController enemy) {
            Debug.Log("Entered chase state.");

        }

        public override void UpdateState(EnemyController enemy) {
            Movement movement = enemy.movement;
            GameObject player = enemy.player;
            movement.MoveToDestination(player.transform.position);
        }


    }
}