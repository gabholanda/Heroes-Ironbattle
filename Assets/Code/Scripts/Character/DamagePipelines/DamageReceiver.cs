using UnityEngine;

using System.Collections.Generic;
public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    protected GameObject popUpPrefab;
    protected StateMachine stateMachine;
    protected ResourcesStats resources;
    protected DefensiveStats defensesResistances;
    protected Dictionary<string, Element> elements;
    [SerializeField]
    protected GameEvent onHitEvent;

    [SerializeField]
    protected AudioSource onHitSource;

    protected virtual void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        (resources, defensesResistances, elements) = stateMachine.stats;
    }

    public virtual DamageReceiver ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
        SetMinimumDamage(ref finalDamage);
        InstantiateDamagePopUp(finalDamage);
        damageResources(resources, finalDamage);
        return this;
    }

    public virtual DamageReceiver ReceiveDamage(float damage, DamageType type, Element element, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, type, element);
        SetMinimumDamage(ref finalDamage);
        InstantiateDamagePopUp(finalDamage);
        damageResources(resources, finalDamage);
        return this;
    }

    protected void SetMinimumDamage(ref int finalDamage)
    {
        if (DamageIsBelowMinimum(finalDamage)) finalDamage = 1;
    }

    public int MitigateDamage(float damage, DamageType type, Element element)
    {
        if (type == DamageType.Magical)
        {
            return Mathf.RoundToInt(MitigateMagicDamage(damage, element));
        }
        else if (type == DamageType.Physical)
        {
            return Mathf.RoundToInt(MitigatePhysicalDamage(damage, element));
        }
        else
        {
            return Mathf.RoundToInt(MitigateTrueDamage(damage, element));
        }
    }

    protected bool IsDead()
    {
        return resources.CurrentHealth <= 0;
    }

    public bool IsAlive()
    {
        return !IsDead();
    }

    protected virtual void DoDeathProcedures()
    {
        stateMachine.isDead = true;
        stateMachine.ChangeState("Dying");
        stateMachine.CancelInvoke();
        stateMachine.StopAllCoroutines();
    }

    protected bool DamageIsBelowMinimum(float finalDamage)
    {
        return finalDamage <= 0;
    }

    public DamageReceiver CheckDeath()
    {
        if (IsDead() && !stateMachine.isDead)
        {
            DoDeathProcedures();
        }
        return this;
    }
    public DamageReceiver TriggerEvent()
    {
        onHitEvent?.Raise();
        return this;
    }

    public DamageReceiver PlaySound()
    {
        onHitSource?.Play();
        return this;
    }

    protected void InstantiateDamagePopUp(float finalDamage)
    {
        GameObject popUp = Instantiate(popUpPrefab, transform);
        popUp.GetComponent<DamagePopUp>().SetDamageText(finalDamage);
    }


    private float MitigatePhysicalDamage(float damage, Element element)
    {
        int physicalDefense = defensesResistances.Armor;
        float elementalResistance = 1f - element.resistance;
        damage = (damage - physicalDefense) * elementalResistance;
        return damage;
    }

    private float MitigateMagicDamage(float damage, Element element)
    {
        int magicResistance = defensesResistances.MagicResistance;
        float elementalResistance = 1f - element.resistance;
        damage = (damage - magicResistance) * elementalResistance;
        return damage;
    }

    private float MitigateTrueDamage(float damage, Element element)
    {
        float elementalResistance = 1f - element.resistance;
        damage *= elementalResistance;
        return damage;
    }
}
