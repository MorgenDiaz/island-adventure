using UnityEngine;

namespace RPG.Character {
    public interface IEnemyController {
        GameObject Player { get; }
        Movement MovementComponent { get; }
        Health HealthComponent { get; }
        Combat CombatComponent { get; }
        float ChaseRange { get; }
        Vector3 OriginalPosition { get; }

        CharacterStatsSO Stats { get; }
    }

    public interface IPatrollingEnemyController : IEnemyController {

    }
}