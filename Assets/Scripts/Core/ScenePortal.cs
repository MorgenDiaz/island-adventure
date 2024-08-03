using RPG.Core;
using RPG.Utility;
using UnityEngine;

namespace RPG.Core {
    public class ScenePortal : MonoBehaviour {
        [SerializeField]
        private int _sceneId;
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag(Constants.Tags.PLAYER)) return;

            EventManager.TriggerOnEnterPortal(other, _sceneId);
            SceneTransition.Initiate(_sceneId);
        }
    }
}