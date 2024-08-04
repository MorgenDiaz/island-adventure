using System.Collections.Generic;
using Newtonsoft.Json;
using RPG.Core;
using UnityEngine;

namespace RPG.Core {
    public class GameManager : MonoBehaviour {
        private List<ISaveable> saveableObjects = new();

        private void OnEnable() {
            EventManager.OnEnterPortal += HandlePortalEntered;
            EventManager.OnRegisterSaveableObject += HandleSaveableObjectRegistered;
        }
        private void Awake() {
            //PlayerPrefs.DeleteAll();
        }

        private void OnDisable() {
            EventManager.OnEnterPortal -= HandlePortalEntered;
            EventManager.OnRegisterSaveableObject -= HandleSaveableObjectRegistered;
        }

        private void HandleSaveableObjectRegistered(ISaveable saveable) {
            saveableObjects.Add(saveable);
        }

        private void HandlePortalEntered(Collider playerCollider, int sceneId, Transform portalSpawnPoint) {
            Debug.Log("did intercept portal event!");
            Debug.Log($"There are {saveableObjects.Count} objects to save");
            PlayerPrefs.SetInt("scene_id", sceneId);

            JsonSerializerSettings settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };


            PlayerPrefs.SetString($"spawn_position_{SceneTransition.GetActiveSceneId()}", JsonConvert.SerializeObject(portalSpawnPoint.position, settings));
            PlayerPrefs.SetString($"spawn_rotation_{SceneTransition.GetActiveSceneId()}", JsonConvert.SerializeObject(portalSpawnPoint.rotation, settings));
            PlayerPrefs.Save();

            foreach (ISaveable saveable in saveableObjects) {
                saveable.Save();
            }
        }
    }
}