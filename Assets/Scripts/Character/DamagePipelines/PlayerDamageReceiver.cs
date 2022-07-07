using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField]
    private GameObject healthBarObj;
    private SlidingBar healthBar;
    private PlayerStateMachine sm;

    protected override void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        sm = GetComponent<PlayerStateMachine>();
        (resources, defensesResistances, elementalResistances) = stateMachine.stats;
        healthBar = healthBarObj.GetComponent<SlidingBar>();
    }
    public override DamageReceiver ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
        SetMinimumDamage(ref finalDamage);
        damageResources(resources, finalDamage);
        InstantiateDamagePopUp(finalDamage);
        UpdateUI();
        return this;
    }

    protected override void DoDeathProcedures()
    {
        sm.RemoveState("Controls");
        sm.RemoveState("ClosedMenu");
        sm.characterAnimator.SetAnimation("Dying", false, true, true, false);
        Destroy(sm.characterCombat);
        Destroy(sm.characterMovement);
        Destroy(this);
    }

    private void UpdateUI()
    {
        float normalizedValue = resources.CurrentHealth / resources.MaxHealth;
        healthBar.UpdateBar(normalizedValue);
    }
}
