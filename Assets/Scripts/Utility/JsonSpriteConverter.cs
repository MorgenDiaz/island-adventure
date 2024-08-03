using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace RPG.Utility {
    public class JsonSpriteConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var sprite = value as Sprite;

            if (sprite != null) {
                string spritePath = AssetDatabase.GetAssetPath(sprite);
                writer.WriteValue(spritePath);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            string spritePath = reader.Value.ToString();
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

            return sprite;
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Sprite);
        }
    }
};