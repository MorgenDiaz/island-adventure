using RPG.Utility;
using UnityEngine.InputSystem;

namespace RPG.Character {
    using UnityEngine;

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class ControlledMovement : MonoBehaviour {
        private Animator animatorComponent;
        private Vector3 movementVector;
        public UnityEngine.AI.NavMeshAgent agent;

        private bool _isMoving = false;

        public bool IsMoving {
            get { return _isMoving; }
            private set { _isMoving = value; }
        }

        protected void Awake() {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            animatorComponent = GetComponentInChildren<Animator>();
        }

        private void OnDisable() {
            animatorComponent.SetFloat(Constants.AnimatorParams.SPEED, 0);
        }

        protected void Update() {
            AnimateMovement();
            Move();
            Rotate();
        }

        private void AnimateMovement() {
            float speed = animatorComponent.GetFloat(Constants.AnimatorParams.SPEED);
            float smoothAcceleration = Time.deltaTime * agent.acceleration;

            if (IsMoving) {
                speed += smoothAcceleration;
            }
            else {
                speed -= smoothAcceleration;
            }

            speed = Mathf.Clamp01(speed);

            animatorComponent.SetFloat(Constants.AnimatorParams.SPEED, speed);
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
            if (context.performed) {
                IsMoving = true;
            }
            else if (context.canceled) {
                IsMoving = false;
            }

            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }
    }
}