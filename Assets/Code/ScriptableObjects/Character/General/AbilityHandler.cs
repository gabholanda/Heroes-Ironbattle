using System.Collections;
using System.Collections.Generic;
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

    public List<OnAbilityHitEvent> effectList;
    public List<AbilityHandler> abilitiesToTriggerOnHit;


    [NonSerialized]
    public float currentTime;
    public abstract void Initialize(GameObject t);
    public abstract Ability Execute(GameObject g, Vector2 v2);

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

    public void SetAbilityData(AbilityData _data)
    {
        abilityData = _data;
    }

    public void SetAbility(Ability _ability)
    {
        ability = _ability;
    }

    public AbilityHandler DeepCopy(AbilityHandler copy)
    {
        copy.abilityData = abilityData;
        copy.ability = prefab.GetComponent<Ability>();
        copy.ability.handler = copy;
        copy.prefab = prefab;
        copy.isCoolingDown = isCoolingDown;
        copy.coRunner = coRunner;

        return copy;
    }
}
