using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryManager))]
public class InventoryManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Generate Slots"))
        {
            InventoryManager inventoryManager = target as InventoryManager;
            inventoryManager.GenerateSlots();
        }
    }
}
