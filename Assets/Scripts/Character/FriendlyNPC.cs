using RPG.Utility;
using UnityEngine;

namespace RPG.Character {
    public class FriendlyNPC : MonoBehaviour {
        [SerializeField]

        private GameObject _interactionIndicator;
        [SerializeField]
        private GameObject _dialogIndicator;

        private void Awake() {
            _dialogIndicator.SetActive(false);
        }
        private void OnTriggerEnter(Collider other) {
            Debug.Log("Did enter notify range");
            if (other.CompareTag(Constants.Tags.PLAYER)) {
                _interactionIndicator.SetActive(false);
                _dialogIndicator.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other.CompareTag(Constants.Tags.PLAYER)) {
                _dialogIndicator.SetActive(false);
                _interactionIndicator.SetActive(true);
            }
        }

    }
}