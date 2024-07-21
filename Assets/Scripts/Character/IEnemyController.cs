using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    public interface IEnemyController : IComponent {
        GameObject Player { get; }
        Movement MovementComponent { get; }
        Health HealthComponent { get; }
        INPCCombat CombatComponent { get; }
        float ChaseRange { get; }
        Vector3 OriginalPosition { get; }

        CharacterStatsSO Stats { get; }
    }

    public interface IPatrollingEnemyController : IEnemyController {

    }
}