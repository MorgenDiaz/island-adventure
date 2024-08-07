using RPG.Utility;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Core {
    public class CinematicController : MonoBehaviour {
        private PlayableDirector playableDirector;
        private void Awake() {
            playableDirector = GetComponent<PlayableDirector>();
        }

        void OnEnable() {
            if (playableDirector.playOnAwake) {
                EventManager.TriggerOnStartedCinematic();
            }
            else {
                playableDirector.played += HandleCinematicStarted;
            }

            playableDirector.stopped += HandleCinematicEnded;
        }

        void OnDisable() {
            playableDirector.played -= HandleCinematicStarted;
            playableDirector.stopped -= HandleCinematicEnded;
        }
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag(Constants.Tags.PLAYER)) return;
            playableDirector.Play();
        }

        private void HandleCinematicStarted(PlayableDirector director) {
            EventManager.TriggerOnStartedCinematic();
        }

        private void HandleCinematicEnded(PlayableDirector director) {
            EventManager.TriggerOnEndedCinematic();
            gameObject.SetActive(false);
        }

    }
}