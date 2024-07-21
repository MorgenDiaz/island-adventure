using UnityEngine;

namespace RPG.Character {
    class CloseRangeForwardDamageDealer {
        public void TryToDealDamage(Transform attackerTransform, float attackerDamage) {
            RaycastHit[] targets = Physics.BoxCastAll(
                attackerTransform.position + attackerTransform.forward,
                attackerTransform.localScale / 2,
                attackerTransform.forward,
                attackerTransform.rotation,
                1f
            );

            foreach (RaycastHit target in targets) {
                if (attackerTransform.CompareTag(target.transform.tag)) continue;

                if (target.transform.TryGetComponent<Health>(out Health targetHealth)) {
                    targetHealth.TakeDamage(attackerDamage);
                }
            }
        }
    }
}