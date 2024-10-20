using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem<T> : IEquatable<InventoryItem<T>>
{
    [SerializeField]
    public T Item;
    public float quantity;

    public override bool Equals(object obj)
    {
        return obj is InventoryItem<T> item &&
               EqualityComparer<T>.Default.Equals(Item, item.Item);
    }

    public bool Equals(InventoryItem<T> other)
    {
        return other != null &&
               EqualityComparer<T>.Default.Equals(Item, other.Item) &&
               quantity == other.quantity;
    }

    public override int GetHashCode()
    {
        int hashCode = 285937358;
        hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Item);
        return hashCode;
    }
}
