using RPG.Utility;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Character.NPC {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour {
        public NavMeshAgent agent;
        private Animator animatorComponent;

        private float _maxSpeed = 1f;

        public float MaxSpeed {
            get { return _maxSpeed; }
            set { _maxSpeed = value; }
        }

        private float animationSpeedClamp = 1f;
        protected void Awake() {
            agent = GetComponent<NavMeshAgent>();
            animatorComponent = GetComponentInChildren<Animator>();
        }

        private bool _isMoving = false;

        public bool IsMoving {
            get { return _isMoving; }
            private set { _isMoving = value; }
        }
        private void Start() {
            //disable unities default rotation.
            agent.updateRotation = false;
        }

        private void Update() {
            AnimateMovement();
        }

        public void Rotate(Vector3 newForwardVector) {
            if (newForwardVector == Vector3.zero) return;

            newForwardVector.y = 0;
            Quaternion startAngle = transform.rotation;
            Quaternion endAngle = Quaternion.LookRotation(newForwardVector);
            transform.rotation = Quaternion.Lerp(
                startAngle,
                endAngle,
                Time.deltaTime * agent.angularSpeed
            );

        }
        public void AnimateMovement() {
            float speed = animatorComponent.GetFloat(Constants.AnimatorParams.SPEED);
            float smoothAcceleration = Time.deltaTime * agent.acceleration;

            if (IsMoving) {
                speed += smoothAcceleration;
            }
            else {
                speed -= smoothAcceleration;
            }
            speed = Mathf.Clamp(speed, 0, animationSpeedClamp);

            animatorComponent.SetFloat(Constants.AnimatorParams.SPEED, speed);
        }

        public void MoveToDestination(Vector3 destination) {
            IsMoving = true;
            Vector3 lookDirection = destination - agent.transform.position;
            Rotate(lookDirection);
            agent.SetDestination(destination);
        }

        public void MoveByOffset(Vector3 offset) {

            if (offset == Vector3.zero) return;

            IsMoving = true;
            Rotate(offset);
            agent.Move(offset);
        }

        public void StopMovement() {
            agent.ResetPath();
            IsMoving = false;
        }

        public bool HasReachedDestination() {
            if (agent.pathPending) return false;

            if (agent.remainingDistance > agent.stoppingDistance) return false;

            if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;

            agent.ResetPath();
            IsMoving = false;
            return true;
        }

        public void OverrideAgentSpeed(float newSpeed) {
            agent.speed = newSpeed;
            animationSpeedClamp = agent.speed < MaxSpeed ? 0.5f : 1f;
        }
    }
}