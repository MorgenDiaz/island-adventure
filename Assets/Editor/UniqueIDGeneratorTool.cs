using System;
using RPG.Utility;
using UnityEditor;
using UnityEngine;
public class UniqueIDGeneratorTool : EditorWindow {
    [MenuItem("Custom Tools/ Generate Unique ID")]
    public static void ShowWindow() {
        GetWindow<UniqueIDGeneratorTool>("Unique ID Generator");
    }
    private void OnGUI() {
        if (GUILayout.Button("Generate Unique ID")) {
            GenerateUniqueID();
        }
    }
    private void GenerateUniqueID() {
        if (Selection.activeGameObject != null) {
            string uniqueID = Guid.NewGuid().ToString();
            Undo.RecordObject(Selection.activeGameObject, "Generate Unique ID");

            UniqueID uniqueIdComponent = Selection.activeGameObject.GetComponent<UniqueID>();
            if (uniqueIdComponent == null) {
                uniqueIdComponent = Selection.activeGameObject.AddComponent<UniqueID>();
            }

            uniqueIdComponent.ID = uniqueID;
            EditorUtility.SetDirty(Selection.activeGameObject);
            Debug.Log($"Generated Unique ID {uniqueID}");
        }
        else {
            Debug.LogWarning("No GameObject selected. Please select a GameObject to generate a Unique ID.");
        }
    }
}