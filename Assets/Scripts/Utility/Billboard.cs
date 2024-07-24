using UnityEngine;

namespace RPG.Utility {
    public class Billboard : MonoBehaviour {
        private new GameObject camera;

        private void Awake() {
            camera = GameObject.FindGameObjectWithTag(Constants.Tags.MAIN_CAMERA);
        }

        private void LateUpdate() {
            Vector3 cameraDirection = transform.position + camera.transform.forward;
            transform.LookAt(cameraDirection);
        }
    }
}