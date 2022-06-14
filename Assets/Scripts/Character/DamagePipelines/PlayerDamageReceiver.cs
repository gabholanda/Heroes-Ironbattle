using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField]
    private GameObject healthBarObj;
    private SlidingBar healthBar;

    protected override void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        (resources, defensesResistances, elementalResistances) = stateMachine.stats;
        healthBar = healthBarObj.GetComponent<SlidingBar>();
    }
    public override void ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        if (TargetIsNotDead())
        {
            float finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
            if (DamageIsNegative(finalDamage)) finalDamage = 0;
            InstantiateDamagePopUp(finalDamage);
            damageResources(resources, finalDamage);
            float normalizedValue = resources.CurrentHealth / resources.MaxHealth;
            healthBar.UpdateBar(normalizedValue);
            if (IsDead())
            {
                DoDeathProcedures();
            }
        }
    }
}
