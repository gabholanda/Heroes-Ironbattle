using ScriptableObjectDropdown;
using UnityEngine;

[CreateAssetMenu(fileName = "OnApplyBurnEffectEvent", menuName = "ScriptableObjects/Events/New Burn Event")]
public class OnApplyBurnEffectEvent : OnAbilityHitEvent
{
    public GameObject effectPrefab;
    public float scalingCoeficient;
    public float duration;
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)]
    public Element element;
    public DamageType damageType;

    public bool isStackable;
    public bool isRenewable;
    public int maxStacks;


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
        float characterElementalScalingAffinity = stats.elements[burn.element.name].affinity;

        burn.target = target;
        burn.element = element;
        burn.type = damageType;
        burn.duration = duration * characterElementalScalingAffinity;
        burn.effectValue = Mathf.Round(stats.combatStats.AttackPower * scalingCoeficient * characterElementalScalingAffinity);
        burn.Apply();
    }

    public override void Raise(GameObject caster, Collider2D collision)
    {
        SetStatusEffect(caster, collision.gameObject);
    }
}
