
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace RPG.Core {
    public struct PlayerSpawnPoint {
        public Vector3 SpawnPosition;
        public Quaternion SpawnRotation;

        public PlayerSpawnPoint(Vector3 spawnPosition, Quaternion spawnRotation) {
            SpawnPosition = spawnPosition;
            SpawnRotation = spawnRotation;
        }

    }

    [Serializable]
    public class SceneState {
        public List<string> SlainEnemyIDs = new();
        public Vector3 SpawnPosition;
        public Quaternion spawnRotation;
    }
    public class SceneData {
        readonly string _sceneKey;
        public SceneData(int sceneID) {
            _sceneKey = $"scene_{sceneID}";
        }

        public bool HasStoredData { get; private set; } = false;

        public SceneState _sceneState = new();
        public void Load() {
            if (!PlayerPrefs.HasKey(_sceneKey)) return;

            HasStoredData = true;

            JsonSerializerSettings settings = new() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            string jsonSceneState = PlayerPrefs.GetString(_sceneKey);
            _sceneState = JsonConvert.DeserializeObject<SceneState>(jsonSceneState, settings);
        }

        public void Save() {
            JsonSerializerSettings settings = new() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            string jsonSceneState = JsonConvert.SerializeObject(_sceneState, settings);
            PlayerPrefs.SetString(_sceneKey, jsonSceneState);
        }

        public void AddSlainEnemy(string enemyID) {
            _sceneState.SlainEnemyIDs.Add(enemyID);
        }

        public bool IsEnemySlain(string enemyID) {
            return _sceneState.SlainEnemyIDs.Contains(enemyID);
        }

        public void SetPlayerSpawnData(Vector3 position, Quaternion rotation) {
            _sceneState.SpawnPosition = position;
            _sceneState.spawnRotation = rotation;
        }

        public PlayerSpawnPoint GetPlayerSpawnData() {
            return new PlayerSpawnPoint(_sceneState.SpawnPosition, _sceneState.spawnRotation);
        }
    }
}