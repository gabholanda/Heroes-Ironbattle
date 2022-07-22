using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private List<IEnumerator> coroutines = new List<IEnumerator>();
    public void Run(IEnumerator coroutine)
    {
        coroutines.Add(coroutine);
        StartCoroutine(coroutine);
    }

    private void OnDestroy()
    {
        coroutines.ForEach(coroutine => StopCoroutine(coroutine));
    }
}
