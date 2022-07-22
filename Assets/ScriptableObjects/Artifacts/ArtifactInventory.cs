using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artifact Inventory", menuName = "ScriptableObjects/Inventories/New Artifact Inventory")]
public class ArtifactInventory : Inventory<ArtifactInventoryItem>
{
    public List<ArtifactInventoryItem> Items;
    public GameObject holder;
    public List<Rarity> applyableRarities;
    public override void Add(ArtifactInventoryItem inventoryItem)
    {
        if (!Items.Contains(inventoryItem))
        {
            Items.Add(inventoryItem);
            inventoryItem.Item.Apply(holder);
        }
        else
        {
            ArtifactInventoryItem duplicate = Items.Find(i => i.Item.name == inventoryItem.Item.name);
            Artifact artifact = duplicate.Item;
            duplicate.quantity += 1;
            if (applyableRarities.Contains(duplicate.Item.rarity))
                artifact.Apply(holder);
        }
    }

    public override void Remove(ArtifactInventoryItem inventoryItem)
    {
        if (Items.Contains(inventoryItem))
        {
            Artifact artifact = Items.Find(i => i.Item.name == inventoryItem.Item.name)?.Item;
            inventoryItem.quantity -= 1;
            artifact.Unapply(holder);
            if (inventoryItem.quantity <= 0)
            {
                Items.Remove(inventoryItem);
            }
        }
    }
}
