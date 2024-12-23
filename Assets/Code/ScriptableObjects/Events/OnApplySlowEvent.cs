﻿using ScriptableObjectDropdown;
using UnityEngine;

[CreateAssetMenu(fileName = "OnApplySlowEvent", menuName = "ScriptableObjects/Events/New Slow Event")]
public class OnApplySlowEvent : OnAbilityHitEvent
{
    public GameObject effectPrefab;
    [Range(0f, 1f)]
    public float scalingCoeficient;
    public float duration;
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)] public Element element;
    public DamageType damageType;

    private void SetStatusEffect(GameObject target)
    {
        Slow slow = target.GetComponentInChildren<Slow>();
        if (DoesNotContainEffect(slow))
        {
            GameObject slowObj = Instantiate(effectPrefab, target.transform);
            slow = slowObj.GetComponent<Slow>();
            slow.target = target;
            slow.element = ScriptableObject.Instantiate(element);
            slow.type = DamageType.Magical;
            slow.duration = 10f;
            slow.effectValue = scalingCoeficient;
            slow.Apply();
        }
        else
        {
            slow.Renew();
        }
    }

    public bool DoesNotContainEffect(StatusEffect effect)
    {
        return effect is null;
    }

    public override void Raise(GameObject caster, Collider2D collision)
    {
        SetStatusEffect(collision.gameObject);
    }
}
