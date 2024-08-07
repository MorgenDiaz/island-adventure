using System;
using RPG.Core;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character.Enemy {
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Patrol))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(INPCCombat))]
    public class PatrollingEnemyController : MonoBehaviour, IEnemyController {

        public IEnemyComponents Components { get; } = new EnemyComponents();

        private Patrol _patrol;

        public Patrol PatrolComponent {
            get { return _patrol; }
            private set { _patrol = value; }
        }

        public float _chaseRange = 2.5f;

        public float ChaseRange {
            get { return _chaseRange; }
            private set { _chaseRange = value; }
        }
        public float _attackRange = 0.75f;

        public float AttackRange {
            get { return _attackRange; }
            private set { _attackRange = value; }
        }
        [SerializeField]
        private AudioClip _deathSound;
        public AudioClip DeathSound { get { return _deathSound; } set { _deathSound = value; } }
        private float _distanceFromPlayer;
        public float DistanceFromPlayer {
            get { return _distanceFromPlayer; }
            private set { _distanceFromPlayer = value; }
        }

        private Vector3 _originalPosition;
        public Vector3 OriginalPosition {
            get { return _originalPosition; }
            private set { _originalPosition = value; }
        }

        [SerializeField]
        private CharacterStatsSO _stats;

        public CharacterStatsSO Stats {
            get { return _stats; }
            private set { _stats = value; }
        }

        private IAIState currentState;
        private AIReturnState returnState;
        private AIPatrolState patrolState;
        private AIChaseState chaseState;
        private AIAttackState attackState;
        private AIDefeatedState defeatedState;

        protected void Awake() {
            if (Stats == null) {
                Debug.LogWarning($"{name} does not have character stats.");
            }

            Components.InitializeFromGameObject(gameObject);
            PatrolComponent = GetComponent<Patrol>();

            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            if (gameManager.IsEnemySlain(Components.IDComponent.ID)) Destroy(gameObject);

            returnState = new(this);
            patrolState = new(this);
            chaseState = new(this);
            attackState = new(this);
            defeatedState = new(this);

            currentState = returnState;
        }

        protected void Start() {
            Components.LoadCharacterStats(Stats);

            OriginalPosition = PatrolComponent.GetPatrolStartPosition();
            currentState.EnterState();
        }

        protected void Update() {
            CalculateDistanceFromPlayer();

            if (Components.HealthComponent.IsDefeated) {
                SwitchState(defeatedState);
            }
            else if (_distanceFromPlayer <= AttackRange) {
                SwitchState(attackState);
            }
            else if (_distanceFromPlayer <= ChaseRange) {
                SwitchState(chaseState);
            }
            else if (Components.MovementComponent.HasReachedDestination()) {
                SwitchState(patrolState);
            }
            else {
                SwitchState(returnState);
            }

            currentState.UpdateState();

        }
        private void SwitchState(IAIState state) {
            if (currentState == state) return;

            currentState.ExitState();
            currentState = state;
            currentState.EnterState();
        }

        private void CalculateDistanceFromPlayer() {
            if (Components.Player == null) return;

            Vector3 playerPosition = Components.Player.transform.position;
            _distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, ChaseRange);
        }
    }
}