using UnityEngine;
using UnityEngine.Splines;

namespace RPG.Character {
    public class Patrol : MonoBehaviour {
        [SerializeField] private GameObject splinePath;
        private SplineContainer splineComponent;

        private void Awake() {
            string SPLINE_COMPONENT_REQUIRED_MESSAGE = $"{name} does not have a reference to a spline. ";

            if (splinePath == null) {
                Debug.LogWarning(SPLINE_COMPONENT_REQUIRED_MESSAGE);
            }
            splineComponent = splinePath.GetComponent<SplineContainer>();
        }

        public Vector3 GetNextPosition() {
            return splineComponent.EvaluatePosition(0);
        }
    }
}