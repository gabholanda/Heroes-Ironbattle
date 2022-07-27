using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = ExplosionFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
        source = GetComponent<AudioSource>();
        source.clip = handler.GetAbilityData().onCastSound;
        source.Play();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
            AfterHit(collider);
        }
    }

    private float ExplosionFormula(Ability ability)
    {
        CombatStats stats = caster.GetComponent<StateMachine>().stats.combatStats;
        int attribute = stats.Dexterity + stats.Strength + stats.Intelligence;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(attribute * scalingCoeficient);
    }

}
