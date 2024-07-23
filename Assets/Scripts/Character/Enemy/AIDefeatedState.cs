using System;
using UnityEngine;
namespace RPG.Character.Enemy {
    public class AIDefeatedState : IAIState {
        public void EnterState() {
            //do nothing, your dead!
        }

        public void UpdateState() {
            Debug.Log("defeated!");
        }
        public void ExitState() {
            //no special cleanup, no state transitions occur after defeat.
        }
    }
}