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
    public override DamageReceiver ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
        if (DamageIsNegative(finalDamage)) finalDamage = 0;
        damageResources(resources, finalDamage);
        InstantiateDamagePopUp(finalDamage);
        UpdateUI();
        return this;
    }

    private void UpdateUI()
    {
        float normalizedValue = resources.CurrentHealth / resources.MaxHealth;
        healthBar.UpdateBar(normalizedValue);
    }
}
