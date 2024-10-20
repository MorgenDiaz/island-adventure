using UnityEngine;
using UnityEngine.Splines;

namespace RPG.Character.NPC {
    public class Patrol : MonoBehaviour {
        [SerializeField] private GameObject splinePath;

        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;
        private float elapsedWalkTime = 0f;
        private float elapsedPauseTime = 0f;
        private bool isWalking = true;
        private SplineContainer splineComponent;
        private float splinePosition = 0f;
        private float pathLength;
        private float lengthWalked = 0f;
        private void Awake() {
            string SPLINE_COMPONENT_REQUIRED_MESSAGE = $"{name} does not have a reference to a spline. ";

            if (splinePath == null) {
                Debug.LogWarning(SPLINE_COMPONENT_REQUIRED_MESSAGE);
            }

            splineComponent = splinePath.GetComponent<SplineContainer>();
            pathLength = splineComponent.CalculateLength();
        }

        public Vector3 GetPatrolStartPosition() {
            return splineComponent.EvaluatePosition(0);
        }

        public Vector3 GetNextPosition(float movementSpeed) {
            Vector3 position = splineComponent.EvaluatePosition(splinePosition);

            if (elapsedWalkTime >= walkDuration) {
                isWalking = false;
                elapsedWalkTime = 0f;
            }

            if (!isWalking) {
                elapsedPauseTime += Time.deltaTime;

                if (elapsedPauseTime >= pauseDuration) {
                    isWalking = true;
                    elapsedPauseTime = 0f;
                }

                return position;
            }

            elapsedWalkTime += Time.deltaTime;
            CalculateNextPosition(movementSpeed);

            return position;
        }

        private void CalculateNextPosition(float movementSpeed) {

            lengthWalked += Time.deltaTime * movementSpeed;

            if (lengthWalked > pathLength) {
                lengthWalked = 0f;
            }

            splinePosition = Mathf.Clamp01(lengthWalked / pathLength);
        }

        public void ResetPatrol() {
            elapsedWalkTime = 0f;
            elapsedPauseTime = 0f;
            isWalking = true;
            lengthWalked = 0f;
            splinePosition = 0f;
        }


    }
}