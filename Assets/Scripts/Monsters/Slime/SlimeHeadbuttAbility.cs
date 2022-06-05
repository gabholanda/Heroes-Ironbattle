using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHeadbuttAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    private int notHit;
    private void OnEnable()
    {
        notHit = 0;
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = HeadbuttFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && notHit == 0)
        {
            notHit = 1;
            DamageReceiver receiver = collision.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
        }
    }

    private float HeadbuttFormula(Ability ability)
    {
        int strength = caster.GetComponent<StateMachine>().stats.combatStats.Strength;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(strength * scalingCoeficient);
    }
}
