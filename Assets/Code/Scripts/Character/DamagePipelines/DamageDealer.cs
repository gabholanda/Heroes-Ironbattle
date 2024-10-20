using UnityEngine;

public delegate float DamageFormula(Ability ability, GameObject caster);
public delegate void DamageResources(ResourcesStats stats, float finalDamage);
public class DamageDealer : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public DamageFormula damageFormula;
    public DamageResources resourcesDamager;

    private void OnEnable()
    {
        damageFormula = DamageFormulas.StandardFormula;
        resourcesDamager = DamageMethods.StandardDamageDealing;
    }


    public void DealDamage(Ability ability, GameObject caster)
    {
        float totalDamage = damageFormula(ability, caster);
        AbilityData abilityData = ability.handler.GetAbilityData();
        if (damageReceiver && damageReceiver.IsAlive())
            damageReceiver
                .CheckDeath()
                .ReceiveDamage(totalDamage, abilityData, resourcesDamager)
                .TriggerEvent()
                .PlaySound()
                .CheckDeath();
    }

    public void SetReceiver(DamageReceiver receiver)
    {
        damageReceiver = receiver;
    }

}