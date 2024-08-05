using System.Collections.Generic;
using Newtonsoft.Json;
using RPG.Core;
using UnityEngine;

namespace RPG.Core {
    public class GameManager : MonoBehaviour {
        private readonly List<ISaveable> saveableObjects = new();
        private List<string> _slainEnemyIDs = new();

        private void OnEnable() {
            EventManager.OnEnterPortal += HandlePortalEntered;
            EventManager.OnRegisterSaveableObject += HandleSaveableObjectRegistered;
            EventManager.OnKillEnemy += HandleEnemyKilled;
        }
        private void Awake() {
            if (PlayerPrefs.HasKey("slain_enemies")) {
                string jsonSlainEnemies = PlayerPrefs.GetString("slain_enemies");
                _slainEnemyIDs = JsonConvert.DeserializeObject<List<string>>(jsonSlainEnemies);
            }
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
            Debug.Log("did intercept portal event!");
            Debug.Log($"There are {saveableObjects.Count} objects to save");
            PlayerPrefs.SetInt("scene_id", sceneId);

            JsonSerializerSettings settings = new() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };


            PlayerPrefs.SetString($"spawn_position_{SceneTransition.GetActiveSceneId()}", JsonConvert.SerializeObject(portalSpawnPoint.position, settings));
            PlayerPrefs.SetString($"spawn_rotation_{SceneTransition.GetActiveSceneId()}", JsonConvert.SerializeObject(portalSpawnPoint.rotation, settings));

            string jsonSlainEnemies = JsonConvert.SerializeObject(_slainEnemyIDs);
            PlayerPrefs.SetString("slain_enemies", jsonSlainEnemies);

            PlayerPrefs.Save();

            foreach (ISaveable saveable in saveableObjects) {
                saveable.Save();
            }
        }

        private void HandleEnemyKilled(string enemyID) {
            Debug.Log($"enemy {enemyID} triggered kill event.");
            _slainEnemyIDs.Add(enemyID);
        }

        public bool IsEnemySlain(string enemyID) {
            return _slainEnemyIDs.Contains(enemyID);
        }
    }
}