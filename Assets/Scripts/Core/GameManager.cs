using System.Collections.Generic;
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

        private void HandlePortalEntered(Collider playerCollider, int sceneId) {
            Debug.Log("did intercept portal event!");
            Debug.Log($"There are {saveableObjects.Count} objects to save");
            PlayerPrefs.SetInt("scene_id", sceneId);
            PlayerPrefs.Save();

            foreach (ISaveable saveable in saveableObjects) {
                saveable.Save();
            }
        }
    }
}