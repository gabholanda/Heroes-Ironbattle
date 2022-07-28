using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ArtifactInventory inventory;
    public List<Rarity> rarities;
    public void StartInventory()
    {
        if (inventory is null)
        {
            inventory = ScriptableObject.CreateInstance<ArtifactInventory>();
            inventory.Items = new List<ArtifactInventoryItem>();
            inventory.applyableRarities = rarities;
        }

        inventory.Items?.ForEach(inventoryItem => inventoryItem.Item.Apply(gameObject));
        inventory.holder = gameObject;
    }

    public void ResetInventory()
    {
        inventory.Items.Clear();
        StartInventory();
    }
}
