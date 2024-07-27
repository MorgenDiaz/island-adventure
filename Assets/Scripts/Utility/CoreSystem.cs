using UnityEngine;

namespace RPG.Utility {
    public static class CoreSystem {
        public static void PauseGame() {
            Time.timeScale = 0;
        }

        public static void ResumeGame() {
            Time.timeScale = 1;
        }
    }
}