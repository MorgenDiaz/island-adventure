using System;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    public class EnemyController : MonoBehaviour {
        private GameObject _player;

        public GameObject player {
            get { return _player; }
            private set { _player = value; }
        }

        private Movement _movement;

        public Movement movement {
            get { return _movement; }
            private set { _movement = value; }
        }

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        private float _distanceFromPlayer;

        // A public property with a public getter and a private setter
        public float distanceFromPlayer {
            get { return _distanceFromPlayer; }
            private set { _distanceFromPlayer = value; }
        }


        private Vector3 _originalPosition;
        public Vector3 originalPosition {
            get { return _originalPosition; }
            private set { _originalPosition = value; }
        }

        private AIBaseState currentState;
        private AIReturnState returnState = new AIReturnState();
        private AIChaseState chaseState = new AIChaseState();

        protected void Awake() {
            currentState = chaseState;
            originalPosition = transform.position;
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movement = GetComponent<Movement>();
        }

        protected void Start() {
            currentState.EnterState(this);
        }

        protected void Update() {
            CalculateDistanceFromPlayer();

            if (distanceFromPlayer <= chaseRange) {
                SwitchState(chaseState);
            }
            else {
                SwitchState(returnState);
            }

            currentState.UpdateState(this);

        }

        private void SwitchState(AIBaseState state) {
            currentState = state;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer() {
            if (player == null) return;

            Vector3 playerPosition = player.transform.position;

            _distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        }


        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}