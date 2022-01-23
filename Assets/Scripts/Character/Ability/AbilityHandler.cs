﻿using System.Collections;
using UnityEngine;

public abstract class AbilityHandler: ScriptableObject
{
    [SerializeField]
    protected AbilityData abilityData;
    [SerializeField]
    protected Ability ability;
    public GameObject prefab;
    public bool isCoolingDown;
    public CoroutineRunner coRunner;
    public abstract void Initialize(GameObject t, Vector2 v2);
    public abstract void Execute(GameObject g, Vector2 v2);

    public IEnumerator StartCooldown()
    {
        float currentTime = 0f;
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
