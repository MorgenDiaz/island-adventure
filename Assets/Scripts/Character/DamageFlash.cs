using System.Collections;
using UnityEngine;
namespace RPG.Character {

    public class DamageFlash : MonoBehaviour {
        private MeshRenderer[] _meshRenderers;
        private Health _healthComponent;
        [field: SerializeField] public float Duration { get; private set; } = .15f;

        [SerializeField] private Color _damageColor = Color.red;

        private Color[][] _meshMaterialOriginalColors;

        private void Awake() {
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();
            _healthComponent = GetComponent<Health>();

            _meshMaterialOriginalColors = new Color[_meshRenderers.Length][];
            CacheOriginalColors();
        }

        private void CacheOriginalColors() {
            for (int r = 0; r < _meshRenderers.Length; r++) {
                MeshRenderer renderer = _meshRenderers[r];
                _meshMaterialOriginalColors[r] = new Color[renderer.materials.Length];

                for (int m = 0; m < renderer.materials.Length; m++) {
                    _meshMaterialOriginalColors[r][m] = renderer.materials[m].color;
                }
            }
        }

        private void OnEnable() {
            _healthComponent.OnTakeDamage += HandleTakeDamage;
        }

        private void OnDisable() {
            _healthComponent.OnTakeDamage -= HandleTakeDamage;
        }
        private void HandleTakeDamage() {
            StartCoroutine(Flash());
        }
        public IEnumerator Flash() {
            ApplyDamageColorToAllMaterials();

            yield return new WaitForSeconds(Duration);

            RestoreAllMaterialColors();
        }

        private void ApplyDamageColorToAllMaterials() {
            foreach (MeshRenderer renderer in _meshRenderers) {
                foreach (Material material in renderer.materials) {
                    material.color = _damageColor;
                }
            }
        }

        private void RestoreAllMaterialColors() {
            for (int r = 0; r < _meshRenderers.Length; r++) {
                MeshRenderer renderer = _meshRenderers[r];

                for (int m = 0; m < renderer.materials.Length; m++) {
                    renderer.materials[m].color = _meshMaterialOriginalColors[r][m];
                }
            }
        }
    }
}