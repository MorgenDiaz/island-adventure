
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RPG.Core;
using RPG.Item;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character.Player {
    public class Inventory : MonoBehaviour, ISaveable {
        [SerializeField]

        List<IItem> _items = new();
        public List<IItem> Items {
            get { return _items; }
            private set { _items = value; }
        }

        private void Awake() {
            EventManager.TriggerOnRegisterSaveable(this);
            Load();
        }

        public void AddItem(IItem item) {
            Items.Add(item);
        }

        public bool HasItem(IItem item) {
            return Items.Find((inventoryItem) => { return inventoryItem.ItemName == item.ItemName; }) != null;
        }

        public void RemoveItem(IItem item) {
            for (int i = 0; i < Items.Count; i++) {
                if (Items[i].ItemName == item.ItemName) {
                    Items.RemoveAt(i);
                }
            }
        }

        public void Save() {
            JsonSerializerSettings settings = new() {
                Converters = new List<JsonConverter> { new JsonSpriteConverter() }
            };

            string json = JsonConvert.SerializeObject(Items, settings);

            PlayerPrefs.SetString("inventory", json);
            PlayerPrefs.Save();
        }

        public void Load() {
            var settings = new JsonSerializerSettings {
                Converters = new List<JsonConverter> { new JsonItemConverter() }
            };

            string json = PlayerPrefs.GetString("inventory");

            Items = JsonConvert.DeserializeObject<List<IItem>>(json, settings);

            Items ??= new List<IItem>();
        }
    }
}