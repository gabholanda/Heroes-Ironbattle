using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    protected GameObject popUpPrefab;
    protected StateMachine stateMachine;
    protected ResourcesStats resources;
    protected DefenseResistances defensesResistances;
    protected ElementalResistances elementalResistances;

    protected virtual void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        (resources, defensesResistances, elementalResistances) = stateMachine.stats;
    }

    public virtual void ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        if (TargetIsNotDead())
        {
            float finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
            if (DamageIsNegative(finalDamage)) finalDamage = 0;
            InstantiateDamagePopUp(finalDamage);
            damageResources(resources, finalDamage);
            if (IsDead())
            {
                DoDeathProcedures();
            }
        }
    }

    public void ReceiveDamage(float damage, DamageType type, ElementType element, DamageResources damageResources)
    {
        if (TargetIsNotDead())
        {
            float finalDamage = MitigateDamage(damage, type, element);
            if (DamageIsNegative(finalDamage)) finalDamage = 0;
            InstantiateDamagePopUp(finalDamage);
            damageResources(resources, finalDamage);
            if (IsDead())
            {
                Debug.Log("Died");
                DoDeathProcedures();
            }
        }
    }

    public float MitigateDamage(float damage, DamageType type, ElementType element)
    {
        if (type == DamageType.Magical)
        {
            return MitigateMagicDamage(damage, element);
        }
        else if (type == DamageType.Physical)
        {
            return MitigatePhysicalDamage(damage, element);
        }
        else
        {
            return MitigateTrueDamage(damage, element);
        }
    }

    private float MitigatePhysicalDamage(float damage, ElementType element)
    {
        int physicalDefense = defensesResistances.Defense;
        float elementalResistance = 1f - GetElementResistance(element);
        damage -= physicalDefense * elementalResistance;
        return damage;
    }

    private float MitigateMagicDamage(float damage, ElementType element)
    {
        int magicResistance = defensesResistances.MagicResistance;
        float elementalResistance = 1f - GetElementResistance(element);
        damage -= magicResistance * elementalResistance;
        return damage;
    }

    private float MitigateTrueDamage(float damage, ElementType element)
    {
        float elementalResistance = 1f - GetElementResistance(element);
        damage *= elementalResistance;
        return damage;
    }

    private float GetElementResistance(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                return elementalResistances.Fire;
            case ElementType.Ice:
                return elementalResistances.Ice;
            case ElementType.Dark:
                return elementalResistances.Dark;
            case ElementType.Lightning:
                return elementalResistances.Lightning;
            default:
                return 0f;
        }
    }

    protected bool TargetIsNotDead()
    {
        return !stateMachine.isDead;
    }

    protected bool IsDead()
    {
        return resources.CurrentHealth < 0;
    }

    protected void DoDeathProcedures()
    {
        stateMachine.isDead = true;
        stateMachine.ChangeState("Dying");
    }

    protected bool DamageIsNegative(float finalDamage)
    {
        return finalDamage < 0;
    }

    protected void InstantiateDamagePopUp(float finalDamage)
    {
        GameObject popUp = Instantiate(popUpPrefab, transform);
        popUp.GetComponent<DamagePopUp>().SetDamageText(finalDamage);
    }
}
