using UnityEngine;

namespace RPG.Utility {
    public interface IComponent {
#pragma warning disable IDE1006
        Transform transform { get; }
    }
}