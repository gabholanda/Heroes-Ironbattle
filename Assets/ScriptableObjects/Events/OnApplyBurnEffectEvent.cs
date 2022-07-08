using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnApplyBurnEffectEvent", menuName = "ScriptableObjects/Events/New Burn Event")]
public class OnApplyBurnEffectEvent : OnAbilityHitEvent
{
    public GameObject effectPrefab;
    public float scalingCoeficient;
    public float duration;
    public ElementType element;
    public DamageType damageType;

    public bool isStackable;
    public bool isRenewable;
    public int maxStacks;

    public override void Raise(Collider2D collision) { }

    private void SetStatusEffect(GameObject caster, GameObject target)
    {
        Burn[] burn = target.GetComponentsInChildren<Burn>();
        if (isStackable && burn.Length < maxStacks)
        {
            ApplyEffect(caster, target);
            return;
        }
        if (burn.Length == 0)
        {
            ApplyEffect(caster, target);
            return;
        }
        if (isRenewable)
        {
            for (int i = 0; i < burn.Length; i++)
            {
                burn[i].Renew();
            }
        }
    }

    private void ApplyEffect(GameObject caster, GameObject target)
    {
        GameObject burnObj = Instantiate(effectPrefab, target.transform);
        Burn burn = burnObj.GetComponent<Burn>();
        CharacterStats stats = caster.GetComponent<StateMachine>().stats;
        float characterElementalScalingAffinity = stats.elementalAffinities[burn.element];

        burn.target = target;
        burn.element = element;
        burn.type = damageType;
        burn.duration = duration * characterElementalScalingAffinity;
        int intelligence = stats.combatStats.Intelligence;
        burn.effectValue = Mathf.Round(intelligence * scalingCoeficient * characterElementalScalingAffinity);
        burn.Apply();
    }

    public override void Raise(Collider2D collision, GameObject caster)
    {
        SetStatusEffect(caster, collision.gameObject);
    }
}
