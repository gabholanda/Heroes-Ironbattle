using UnityEngine;

public class CharacterDamageReceiver : DamageReceiver
{
    [SerializeField]
    private GameObject healthBarObj;
    private SlidingBar healthBar;
    private CharacterStateMachine sm;

    protected override void Start()
    {
        stateMachine = GetComponent<CharacterStateMachine>();
        sm = GetComponent<CharacterStateMachine>();
        (resources, defensesResistances, elementalResistances) = stateMachine.stats;
        healthBar = healthBarObj.GetComponent<SlidingBar>();
    }
    public override DamageReceiver ReceiveDamage(float damage, AbilityData abilityData, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, abilityData.type, abilityData.element);
        SetMinimumDamage(ref finalDamage);
        damageResources(resources, finalDamage);
        sm.eventManager.events["Hurt"].Raise();
        InstantiateDamagePopUp(finalDamage);
        UpdateUI();
        return this;
    }

    protected override void DoDeathProcedures()
    {
        sm.RemoveState("Controls");
        sm.RemoveState("ClosedMenu");
        sm.animator.SetAnimation("Dying", false, true, true, false);
        Destroy(sm.combat);
        Destroy(sm.movement);
        Destroy(this);
    }

    private void UpdateUI()
    {
        float normalizedValue = resources.CurrentHealth / resources.MaxHealth;
        healthBar.UpdateBar(normalizedValue);
    }
}
