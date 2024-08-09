using RPG.Utility;
using UnityEngine;

namespace RPG.Character.NPC {
    public class EnemyComponents : IEnemyComponents {
        private GameObject _player;

        public GameObject Player {
            get { return _player; }
            private set { _player = value; }
        }

        public UniqueID IDComponent { get; private set; }
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

        private INPCCombat _combat;

        public INPCCombat CombatComponent {
            get { return _combat; }
            private set { _combat = value; }
        }

        private NPCHealthBar healthBarComponent;

        public AudioSource AudioSourceComponent { get; private set; }

        public void InitializeFromGameObject(GameObject enemyGameObject) {

            Player = GameObject.FindWithTag(Constants.Tags.PLAYER);

            IDComponent = enemyGameObject.GetComponent<UniqueID>();
            MovementComponent = enemyGameObject.GetComponent<Movement>();
            HealthComponent = enemyGameObject.GetComponent<Health>();
            CombatComponent = enemyGameObject.GetComponent<INPCCombat>();
            healthBarComponent = enemyGameObject.GetComponent<NPCHealthBar>();
            AudioSourceComponent = enemyGameObject.GetComponent<AudioSource>();
        }

        public void LoadCharacterStats(CharacterStatsSO stats) {
            MovementComponent.MaxSpeed = stats.runSpeed;
            HealthComponent.MaxHealthPoints = stats.health;
            HealthComponent.HealthPoints = stats.health;
            healthBarComponent.MaxHealth = stats.health;
            healthBarComponent.Health = stats.health;

            CombatComponent.Damage = stats.damage;
        }
    }
}