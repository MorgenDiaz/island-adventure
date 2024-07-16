using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace RPG.Character {
    public class Movement : MonoBehaviour {
        private Vector3 movementVector;
        public NavMeshAgent agent;

        protected void Awake() {
            agent = GetComponent<NavMeshAgent>();
        }

        protected void Update() {
            MovePlayer();
        }

        private void MovePlayer() {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }
        public void HandleMove(InputAction.CallbackContext context) {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }
    }
}