using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float yOffset;
    public float xOffset;
    public float duration;
    public float timeCheck;
    public float effectValue;
    public float current = 0f;

    public ElementType element;
    public DamageType type;
    public DamageResources dealer;
    public GameObject target;

    public abstract void Apply();
    public void Renew()
    {
        current = 0f;
    }

    public void IncreaseDuration(float time)
    {
        duration += time;
    }

    public void SetNewDuration(float time)
    {
        duration = time;
    }

    public void Stack(float value)
    {
        effectValue += value;
    }
    public abstract IEnumerator Tick();
}
