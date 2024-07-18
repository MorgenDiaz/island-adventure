using UnityEngine;

namespace RPG.Character {
    public interface IEnemyController {
        GameObject Player { get; }
        Movement MovementComponent { get; }
        float ChaseRange { get; }
        Vector3 OriginalPosition { get; }
    }

    public interface IPatrollingEnemyController : IEnemyController {

    }
}