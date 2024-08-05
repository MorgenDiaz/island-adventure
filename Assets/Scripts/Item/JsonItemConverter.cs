
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RPG.Item;
using RPG.Utility;
using UnityEditor;
using UnityEngine;

namespace RPG.Item {
    public class JsonItemConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var jObject = JObject.FromObject(value, new JsonSerializer {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            jObject.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType != JsonToken.StartObject) return null;

            JObject jObject = JObject.Load(reader);
            int itemType = jObject["ItemType"].ToObject<int>();

            IItem item;

            switch ((ItemType)itemType) {
                case ItemType.KeyItem:
                    item = new KeyItem();
                    PopulateItemDataFromJson(item, jObject);
                    break;
                case ItemType.Weapon:
                    item = new Weapon();
                    PopulateWeaponDataFromJson(item as IWeapon, jObject);
                    break;
                default:
                    // Add cases for other item types
                    throw new Exception("Unknown item type");

            };

            return item;
        }

        private void PopulateItemDataFromJson(IItem item, JObject jObject) {
            item.ItemName = jObject["ItemName"].ToString();
            item.ItemImage = AssetDatabase.LoadAssetAtPath<Sprite>(jObject["ItemImage"].ToString());
            item.Equippable = (bool)jObject["Equippable"];
        }

        private void PopulateWeaponDataFromJson(IWeapon item, JObject jObject) {
            PopulateItemDataFromJson(item, jObject);

            item.GameTag = (string)jObject["GameTag"];
            item.Damage = (int)jObject["Damage"];
            item.IsEquipped = (bool)jObject["IsEquipped"];
        }

        public override bool CanConvert(Type objectType) {
            return typeof(IItem).IsAssignableFrom(objectType);
        }
    }


}