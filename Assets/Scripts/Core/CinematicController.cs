using RPG.Utility;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Core {
    public class CinematicController : MonoBehaviour {
        private PlayableDirector playableDirector;
        private void Awake() {
            GameObject gameManager = GameObject.FindWithTag(Constants.Tags.GAME_MANAGER);

            if (gameManager.GetComponent<GameManager>().HasSavedSceneData()) {
                gameObject.SetActive(false);
            }

            playableDirector = GetComponent<PlayableDirector>();
        }
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag(Constants.Tags.PLAYER)) return;
            playableDirector.Play();

        }

    }
}