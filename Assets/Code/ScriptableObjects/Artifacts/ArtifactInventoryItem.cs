using System;
using UnityEngine;

[Serializable]
public class ArtifactInventoryItem : InventoryItem<Artifact>
{
    public ArtifactInventoryItem(Artifact _Item)
    {
        Item = _Item;
        quantity = 1;
    }
}
