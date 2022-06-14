using UnityEngine;

public delegate float DamageFormula(Ability ability);
public delegate void DamageResources(ResourcesStats stats, float finalDamage);
public class DamageDealer : MonoBehaviour
{
    public DamageReceiver damageReceiver;


    public void DealDamage(Ability ability, DamageFormula damageFormula, DamageResources dealer)
    {
        float totalDamage = damageFormula(ability);
        AbilityData abilityData = ability.handler.GetAbilityData();
        damageReceiver.ReceiveDamage(totalDamage, abilityData, dealer);
    }

    public void SetReceiver(DamageReceiver receiver)
    {
        damageReceiver = receiver;
    }

}