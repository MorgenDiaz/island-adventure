using RPG.Core;
using UnityEngine;

namespace RPG.Character.NPC {
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(INPCCombat))]
    public class EnemyController : MonoBehaviour, IEnemyController {
        public IEnemyComponents Components { get; } = new EnemyComponents();

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
        private AIReturnState returnState;
        private AIChaseState chaseState;
        private AIAttackState attackState;
        private AIDefeatedState defeatedState;
        protected void Awake() {
            Components.InitializeFromGameObject(gameObject);

            if (Stats == null) {
                Debug.LogWarning($"{name} does not have character stats.");
            }
            if (Components.IDComponent == null) {
                Debug.LogWarning($"{name} does not have a unique id.");
            }

            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            if (gameManager.IsEnemySlain(Components.IDComponent.ID)) Destroy(gameObject);

            returnState = new(this);
            chaseState = new(this);
            attackState = new(this);
            defeatedState = new(this);

            currentState = returnState;
            OriginalPosition = transform.position;

        }
        private void Start() {
            Components.LoadCharacterStats(Stats);
            currentState.EnterState();
        }

        protected void Update() {
            CalculateDistanceFromPlayer();

            if (Components.HealthComponent.IsDefeated) {
                SwitchState(defeatedState);
            }
            else if (DistanceFromPlayer <= AttackRange) {
                SwitchState(attackState);
            }
            else if (DistanceFromPlayer <= ChaseRange) {
                SwitchState(chaseState);
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