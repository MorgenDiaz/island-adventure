using UnityEngine;
using UnityEngine.AI;

namespace RPG.Character {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour {
        public NavMeshAgent agent;

        protected void Awake() {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start() {
            //disable unities default rotation.
            agent.updateRotation = false;
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

        public void MoveToDestination(Vector3 destination) {
            Vector3 lookDirection = destination - agent.transform.position;
            Rotate(lookDirection);
            agent.SetDestination(destination);
        }

        public void MoveByOffset(Vector3 offset) {

            if (offset == Vector3.zero) {
                Debug.LogError("ZERONI");
            }
            Rotate(offset);
            agent.Move(offset);
        }

        public void StopMovement() {
            agent.ResetPath();
        }

        public bool HasReachedDestination() {
            if (agent.pathPending) return false;

            if (agent.remainingDistance > agent.stoppingDistance) return false;

            if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;

            agent.ResetPath();
            return true;
        }

        public void OverrideAgentSpeed(float newSpeed) {
            agent.speed = newSpeed;
        }
    }
}