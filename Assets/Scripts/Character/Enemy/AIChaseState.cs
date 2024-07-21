using UnityEngine;

namespace RPG.Character.Enemy {
    class AIChaseState : IAIState {
        public void EnterState(IEnemyController enemy) {
            enemy.MovementComponent.OverrideAgentSpeed(enemy.Stats.runSpeed);
        }

        public void UpdateState(IEnemyController enemy) {

            Movement movement = enemy.MovementComponent;
            GameObject player = enemy.Player;

            movement.MoveToDestination(player.transform.position);
        }


    }
}