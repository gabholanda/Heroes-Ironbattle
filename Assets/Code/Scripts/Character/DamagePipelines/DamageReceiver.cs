using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    protected GameObject popUpPrefab;
    protected StateMachine stateMachine;
    protected ResourcesStats resources;
    protected DefenseResistances defensesResistances;
    protected ElementalResistances elementalResistances;
    [SerializeField]
    protected GameEvent onHitEvent;

    [SerializeField]
    protected AudioSource onHitSource;

    protected virtual void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        (resources, defensesResistances, elementalResistances) = stateMachine.stats;
    }

    public virtual DamageReceiver ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
        SetMinimumDamage(ref finalDamage);
        InstantiateDamagePopUp(finalDamage);
        damageResources(resources, finalDamage);
        return this;
    }

    public virtual DamageReceiver ReceiveDamage(float damage, DamageType type, ElementType element, DamageResources damageResources)
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

    public int MitigateDamage(float damage, DamageType type, ElementType element)
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
        return resources.CurrentHealth < 0;
    }

    protected virtual void DoDeathProcedures()
    {
        stateMachine.isDead = true;
        stateMachine.ChangeState("Dying");
    }

    protected bool DamageIsBelowMinimum(float finalDamage)
    {
        return finalDamage <= 0;
    }

    public DamageReceiver CheckDeath()
    {
        if (IsDead())
        {
            DoDeathProcedures();
        }
        return this;
    }
    public DamageReceiver TriggerEvent()
    {
        onHitEvent.Raise();
        return this;
    }

    public DamageReceiver PlaySound()
    {
        onHitSource.Play();
        return this;
    }

    protected void InstantiateDamagePopUp(float finalDamage)
    {
        GameObject popUp = Instantiate(popUpPrefab, transform);
        popUp.GetComponent<DamagePopUp>().SetDamageText(finalDamage);
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
        damage -= (magicResistance) * elementalResistance;
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
                return elementalResistances.Fire / 100;
            case ElementType.Ice:
                return elementalResistances.Ice / 100;
            case ElementType.Dark:
                return elementalResistances.Dark / 100;
            case ElementType.Lightning:
                return elementalResistances.Lightning / 100;
            default:
                return 0f;
        }
    }
}
