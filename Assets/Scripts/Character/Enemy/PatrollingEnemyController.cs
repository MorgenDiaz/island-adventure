using System;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Patrol))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ICombat))]
    public class PatrollingEnemyController : MonoBehaviour, IEnemyController {

        private GameObject _player;

        public GameObject Player {
            get { return _player; }
            private set { _player = value; }
        }

        private Movement _movement;

        public Movement MovementComponent {
            get { return _movement; }
            private set { _movement = value; }
        }
        private Health _health;

        public Health HealthComponent {
            get { return _health; }
            private set { _health = value; }
        }

        private ICombat _combat;

        public ICombat CombatComponent {
            get { return _combat; }
            private set { _combat = value; }
        }
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

        private float _distanceFromPlayer;

        // A public property with a public getter and a private setter
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
        private readonly AIReturnState returnState = new();
        private readonly AIPatrolState patrolState = new();
        private readonly AIChaseState chaseState = new();
        private readonly AIAttackState attackState = new();


        protected void Awake() {
            currentState = returnState;
            Player = GameObject.FindWithTag(Constants.Tags.PLAYER);
            MovementComponent = GetComponent<Movement>();
            HealthComponent = GetComponent<Health>();
            CombatComponent = GetComponent<ICombat>();
            PatrolComponent = GetComponent<Patrol>();
        }

        protected void Start() {
            MovementComponent.MaxSpeed = Stats.runSpeed;
            HealthComponent.healthPoints = Stats.health;
            CombatComponent.Damage = Stats.damage;
            OriginalPosition = PatrolComponent.getPatrolStartPosition();
            currentState.EnterState(this);
        }

        protected void Update() {
            CalculateDistanceFromPlayer();

            if (_distanceFromPlayer <= AttackRange) {
                SwitchState(attackState);
            }
            else if (_distanceFromPlayer <= ChaseRange) {
                SwitchState(chaseState);
            }
            else if (MovementComponent.HasReachedDestination()) {
                SwitchState(patrolState);
            }
            else {
                SwitchState(returnState);
            }

            currentState.UpdateState(this);

        }

        private void SwitchState(IAIState state) {
            if (currentState == state) return;
            currentState = state;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer() {
            if (Player == null) return;

            Vector3 playerPosition = Player.transform.position;
            _distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
        }


        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, ChaseRange);
        }
    }
}