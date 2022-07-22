using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    public delegate void InteractDelegate(GameObject g);
    public event InteractDelegate OnInteract;
    public void Interact(GameObject p)
    {
        OnInteract?.Invoke(p);
    }
}
