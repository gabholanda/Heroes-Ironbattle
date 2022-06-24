using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterEvents : MonoBehaviour
{
    public delegate void monsterDeathDelegate(GameObject g);
    public event monsterDeathDelegate MonsterDeathEvent;
    private void OnDestroy()
    {
        if (MonsterDeathEvent != null)
            MonsterDeathEvent(gameObject);
    }



}
