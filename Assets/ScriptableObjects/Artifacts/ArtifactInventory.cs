using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artifact Inventory", menuName = "ScriptableObjects/Inventories/New Artifact Inventory")]
public class ArtifactInventory : Inventory<Artifact>
{
    public GameObject holder;
    public List<Rarity> applyableRarities;
    public override void Add(Artifact item)
    {
        if (!Items.Contains(item))
        {
            Items.Add(item);
            item.Apply(holder);
        }
        else
        {
            Artifact artifact = Items.Find(i => i.name == item.name);
            artifact.quantity += 1;
            if (applyableRarities.Contains(item.rarity))
                artifact.Apply(holder);
        }
    }

    public override void Remove(Artifact item)
    {
        if (Items.Contains(item))
        {
            Artifact artifact = Items.Find(i => i.name == item.name);
            artifact.quantity -= 1;
            artifact.Unapply(holder);
            if (artifact.quantity <= 0)
            {
                Items.Remove(item);
            }
        }
    }
}
