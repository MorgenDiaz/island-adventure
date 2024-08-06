using System.Collections.Generic;
using Newtonsoft.Json;
using RPG.Core;
using UnityEngine;

namespace RPG.Core {
    public class GameManager : MonoBehaviour {
        private readonly List<ISaveable> saveableObjects = new();
        private SceneData _sceneData;

        private void OnEnable() {
            EventManager.OnEnterPortal += HandlePortalEntered;
            EventManager.OnRegisterSaveableObject += HandleSaveableObjectRegistered;
            EventManager.OnKillEnemy += HandleEnemyKilled;
        }
        private void Awake() {
            _sceneData = new SceneData(SceneTransition.GetActiveSceneId());
            _sceneData.Load();
        }

        private void OnDisable() {
            EventManager.OnEnterPortal -= HandlePortalEntered;
            EventManager.OnRegisterSaveableObject -= HandleSaveableObjectRegistered;
            EventManager.OnKillEnemy -= HandleEnemyKilled;
        }

        private void HandleSaveableObjectRegistered(ISaveable saveable) {
            saveableObjects.Add(saveable);
        }

        private void HandlePortalEntered(Collider playerCollider, int sceneId, Transform portalSpawnPoint) {
            UpdateLastScene(sceneId);

            _sceneData.SetPlayerSpawnData(portalSpawnPoint.position, portalSpawnPoint.rotation);
            _sceneData.Save();

            foreach (ISaveable saveable in saveableObjects) {
                saveable.Save();
            }
        }

        private void HandleEnemyKilled(string enemyID) {
            _sceneData.AddSlainEnemy(enemyID);
        }

        public bool IsEnemySlain(string enemyID) => _sceneData.IsEnemySlain(enemyID);

        private void UpdateLastScene(int sceneId) {
            PlayerPrefs.SetInt("last_scene", sceneId);
            PlayerPrefs.Save();
        }

        public bool HasSavedGameData() {
            return PlayerPrefs.HasKey("start_save_data");
        }

        public bool HasSavedSceneData() {
            return _sceneData.HasStoredData;
        }

        public PlayerSpawnPoint GetPlayerSpawnData() => _sceneData.GetPlayerSpawnData();
    }
}