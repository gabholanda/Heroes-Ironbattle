using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(InventoryManager))]
public class InventoryManagerEditor : Editor
{
    SerializedProperty inventoryProp;
    SerializedProperty raritiesProp;
    private bool showItems = false;
    void OnEnable()
    {
        inventoryProp = serializedObject.FindProperty("inventory");
        raritiesProp = serializedObject.FindProperty("rarities");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();
        EditorGUILayout.PropertyField(raritiesProp);
        EditorGUILayout.PropertyField(inventoryProp);
        SetShowItemsButton();
        TryShowItems();
        serializedObject.ApplyModifiedProperties();
    }

    private void SetShowItemsButton()
    {
        if (GUILayout.Button("Show Items", GUILayout.Width(100)))
        {
            showItems = !showItems;
        }
    }

    private void TryShowItems()
    {
        if (showItems)
        {
            InventoryManager manager = target as InventoryManager;
            if (manager.inventory && manager.inventory.Items.Count > 0)
            {
                EditorGUILayout.LabelField("Inventory List", EditorStyles.boldLabel);
                for (int i = 0; i < manager.inventory.Items.Count; i++)
                {
                    EditorGUILayout.LabelField(manager.inventory.Items[i].Item.name
                        + " - Quantity: " + manager.inventory.Items[i].quantity + " - " + manager.inventory.Items[i].Item.rarity.name);
                }
            }
            else
            {
                EditorGUILayout.LabelField("There are no items");
            }
        }
    }
}
