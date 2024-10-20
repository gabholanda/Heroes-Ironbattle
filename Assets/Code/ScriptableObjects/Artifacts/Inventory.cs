using System.Collections.Generic;
using UnityEngine;


public abstract class Inventory<T> : ScriptableObject
{
    public abstract void Add(T item);
    public abstract void Remove(T item);
}
