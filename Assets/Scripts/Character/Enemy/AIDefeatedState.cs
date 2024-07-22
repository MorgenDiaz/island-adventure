using System;
using UnityEngine;
namespace RPG.Character.Enemy {
    public class AIDefeatedState : IAIState {
        public void EnterState(IEnemyController enemy) {

        }

        public void UpdateState(IEnemyController enemy) {
            Debug.Log("defeated!");
        }
        public void ExitState(IEnemyController enemy) {

        }
    }
}