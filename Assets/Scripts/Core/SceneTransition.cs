using System.Collections;
using RPG.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core {
    public static class SceneTransition {
        public static IEnumerator Initiate(int sceneIndex) {
            GameObject gameManager = GameObject.FindWithTag(Constants.Tags.GAME_MANAGER);
            AudioSource audioSouce = gameManager.GetComponent<AudioSource>();

            float fadeTransition = 2f;

            while (audioSouce.volume > 0) {
                audioSouce.volume -= Time.deltaTime / fadeTransition;

                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene(sceneIndex);
        }

        public static int GetActiveSceneId() {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}