using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory<T> : ScriptableObject
{
    public List<T> Items = new List<T>();

    public abstract void Add(T item);
    public abstract void Remove(T item);
}
