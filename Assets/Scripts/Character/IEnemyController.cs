using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    public interface IEnemyController : IComponent {
        IEnemyComponents Components { get; }
        float ChaseRange { get; }
        Vector3 OriginalPosition { get; }

        CharacterStatsSO Stats { get; }
    }

    public interface IPatrollingEnemyController : IEnemyController {

    }
}