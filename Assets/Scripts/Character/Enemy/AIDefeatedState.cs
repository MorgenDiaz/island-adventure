using System;
using RPG.Core;
using UnityEngine;
namespace RPG.Character.Enemy {
    public class AIDefeatedState : IAIState {
        private readonly IEnemyController _enemyController;
        public AIDefeatedState(IEnemyController enemyController) {
            _enemyController = enemyController;
        }
        public void EnterState() {
            _enemyController.Components.AudioSourceComponent.clip = _enemyController.DeathSound;
            _enemyController.Components.AudioSourceComponent.Play();
            EventManager.TriggerOnKillEnemy(_enemyController.Components.IDComponent.ID);
        }

        public void UpdateState() {
            //do nothing, your dead!
        }
        public void ExitState() {
            //no special cleanup, no state transitions occur after defeat.
        }
    }
}