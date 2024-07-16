using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour {
        private Vector3 movementVector;
        public NavMeshAgent agent;

        protected void Awake() {
            agent = GetComponent<NavMeshAgent>();
        }

        protected void Update() {
            Move();
            Rotate();
        }

        private void Move() {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        private void Rotate() {
            if (movementVector == Vector3.zero) return;

            Quaternion startAngle = transform.rotation;
            Quaternion endAngle = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Lerp(
                startAngle,
                endAngle,
                Time.deltaTime * agent.angularSpeed
            );

        }
        public void HandleMove(InputAction.CallbackContext context) {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }

        public void MoveToDestination(Vector3 destination) {
            agent.SetDestination(destination);
        }
    }
}