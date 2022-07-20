using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBallAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;

    private void OnEnable()
    {
        newHandler = (ProjectileHandler)handler;
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = MagmaBallFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
        source = GetComponent<AudioSource>();
        source.clip = handler.GetAbilityData().onCastSound;
        source.Play();
    }

    private void FixedUpdate()
    {
        rb.AddForce(newHandler.projectileSpeed * Time.fixedDeltaTime * newHandler.dir);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
            onHitParticles.Play();
            source.clip = handler.GetAbilityData().onHitSound;
            source.Play();
            handler.onHitEvent?.Raise(collider, caster);
        }
    }

    private float MagmaBallFormula(Ability ability)
    {
        CharacterStats stats = caster.GetComponent<StateMachine>().stats;
        int intelligence = stats.combatStats.Intelligence;
        float characterElementalScalingAffinity = stats.elementalAffinities[ability.handler.GetAbilityData().element];
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(intelligence * scalingCoeficient * characterElementalScalingAffinity);
    }
}
