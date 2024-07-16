using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    public class EnemyController : MonoBehaviour {
        private GameObject player;
        private Movement movement;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;
        private float distanceFromPlayer;

        protected void Awake() {
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movement = GetComponent<Movement>();
        }

        protected void Update() {
            CalculateDistanceFromPlayer();
            ChasePlayer();
        }

        private void CalculateDistanceFromPlayer() {
            if (player == null) return;

            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        }

        public float getDistanceFromPlayer() {
            return distanceFromPlayer;
        }

        private void ChasePlayer() {
            if (distanceFromPlayer <= chaseRange) {
                movement.MoveToDestination(player.transform.position);
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}