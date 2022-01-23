using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public void Run(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
