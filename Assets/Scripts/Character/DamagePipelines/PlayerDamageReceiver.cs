using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField]
    private GameObject healthBarObj;
    private HealthBar healthBar;

    protected override void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        (resources, defensesResistances, elementalResistances) = stateMachine.stats;
        healthBar = healthBarObj.GetComponent<HealthBar>();
        healthBar.SetStats(stateMachine.stats.resources);
    }
    public override void ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        if (TargetIsNotDead())
        {
            float finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
            if (DamageIsNegative(finalDamage)) finalDamage = 0;
            InstantiateDamagePopUp(finalDamage);
            damageResources(resources, finalDamage);
            healthBar.UpdateBar();
            if (IsDead())
            {
                DoDeathProcedures();
            }
        }
    }
}
