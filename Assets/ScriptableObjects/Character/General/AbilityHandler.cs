using System.Collections;
using UnityEngine;
using System;

public abstract class AbilityHandler : ScriptableObject
{
    [SerializeField]
    protected AbilityData abilityData;
    [SerializeField]
    protected Ability ability;
    public GameObject prefab;
    public bool isCoolingDown;
    public CoroutineRunner coRunner;

    [Header("Events")]
    public OnAbilityHitEvent onHitEvent;

    [NonSerialized]
    public float currentTime;
    public abstract void Initialize(GameObject t, Vector2 v2);
    public abstract void Execute(GameObject g, Vector2 v2);

    public IEnumerator StartCooldown()
    {
        currentTime = 0f;
        while (abilityData.cooldownDuration > currentTime)
        {
            yield return new WaitForSeconds(0.1f);
            currentTime += 0.1f;
        }
        this.isCoolingDown = false;
    }

    public AbilityData GetAbilityData()
    {
        return abilityData;
    }

    public Ability GetAbility()
    {
        return ability;
    }
}
