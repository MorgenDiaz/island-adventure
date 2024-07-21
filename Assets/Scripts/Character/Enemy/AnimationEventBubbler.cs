
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Character.Enemy {

    public class AnimationEventBubbler : MonoBehaviour {

        public event UnityAction OnBubbleStartAttack = () => { };
        public event UnityAction OnBubbleAttackHit = () => { };
        public event UnityAction OnBubbleCompleteAttack = () => { };
        public event UnityAction OnBubbleDefeated = () => { };
        public void OnStartAttack() {
            OnBubbleStartAttack.Invoke();
        }

        public void OnAttackHit() {
            OnBubbleAttackHit.Invoke();
        }
        public void OnCompleteAttack() {
            OnBubbleCompleteAttack.Invoke();
        }
        public void OnDefeatComplete() {
            OnBubbleDefeated.Invoke();
        }
    }
}