﻿using UnityEngine;

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
            AfterHit(collider);
        }
    }

    private float MagmaBallFormula(Ability ability)
    {
        CombatStats stats = caster.GetComponent<StateMachine>().stats.combatStats;
        int attribute = Mathf.Max(stats.Dexterity, stats.Strength, stats.Intelligence);
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(attribute * scalingCoeficient);
    }
}
