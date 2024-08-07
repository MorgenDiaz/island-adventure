using RPG.Core;
using UnityEngine;

namespace RPG.Utility {
    public class DisableObjectIfSceneReloaded : MonoBehaviour {
        private void Awake() {
            GameObject gameManager = GameObject.FindWithTag(Constants.Tags.GAME_MANAGER);

            if (gameManager.GetComponent<GameManager>().HasSavedSceneData()) {
                gameObject.SetActive(false);
            }
        }
    }
}