using UnityEngine;

namespace RPG.Character.NPC {
    class AIChaseState : IAIState {
        private readonly IEnemyController _enemy;
        public AIChaseState(IEnemyController enemy) {
            _enemy = enemy;
        }
        public void EnterState() {
            _enemy.Components.MovementComponent.OverrideAgentSpeed(_enemy.Stats.runSpeed);
        }

        public void UpdateState() {

            Movement movement = _enemy.Components.MovementComponent;
            GameObject player = _enemy.Components.Player;

            movement.MoveToDestination(player.transform.position);
        }

        public void ExitState() {

        }
    }
}