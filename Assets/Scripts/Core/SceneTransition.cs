using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core {
    public static class SceneTransition {
        public static void initiate(int sceneIndex) {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}