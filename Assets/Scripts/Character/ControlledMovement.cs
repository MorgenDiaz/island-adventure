using UnityEngine.InputSystem;

namespace RPG.Character {
    using UnityEngine;

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class ControlledMovement : MonoBehaviour {

        private Vector3 movementVector;
        public UnityEngine.AI.NavMeshAgent agent;

        protected void Awake() {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        protected void Update() {
            Move();
            Rotate();
        }

        private void Move() {
            Vector3 offset = agent.speed * Time.deltaTime * movementVector;
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
    }
}