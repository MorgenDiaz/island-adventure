using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    public interface IEnemyComponents {
        UniqueID IDComponent { get; }
        GameObject Player { get; }
        Movement MovementComponent { get; }
        Health HealthComponent { get; }
        INPCCombat CombatComponent { get; }
        AudioSource AudioSourceComponent { get; }

        public void InitializeFromGameObject(GameObject enemyGameObject);
        public void LoadCharacterStats(CharacterStatsSO stats);
    }
}