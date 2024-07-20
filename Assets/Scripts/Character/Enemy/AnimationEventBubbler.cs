
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Character.Enemy {

    public class AnimationEventBubbler : MonoBehaviour {

        public event UnityAction OnBubbleStartAttack = () => { };
        public event UnityAction OnBubbleCompleteAttack = () => { };
        public void OnStartAttack() {
            OnBubbleStartAttack.Invoke();
        }

        public void OnCompleteAttack() {
            OnBubbleCompleteAttack.Invoke();
        }
    }
}