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
        (resources, defensesResistances, elements) = stateMachine.stats;
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

    public override DamageReceiver ReceiveDamage(float damage, DamageType type, Element element, DamageResources damageResources)
    {
        int finalDamage = MitigateDamage(damage, type, element);
        SetMinimumDamage(ref finalDamage);
        damageResources(resources, finalDamage);
        sm.eventManager.events["Hurt"].Raise();
        InstantiateDamagePopUp(finalDamage);
        UpdateUI();
        return this;
    }

    protected override void DoDeathProcedures()
    {
        sm.isDead = true;
        sm.movement.enabled = false;
        sm.stats.elements.Clear();
        foreach (var kv in sm.states)
        {
            sm.RemoveState(kv.Key);
        }
        sm.animator.SetAnimation("Dying", false, true, true, false);
        sm.OnPlayerDeath?.Raise();
    }

    private void UpdateUI()
    {
        float normalizedValue = resources.CurrentHealth / resources.MaxHealth;
        healthBar.UpdateBar(normalizedValue);
    }
}
