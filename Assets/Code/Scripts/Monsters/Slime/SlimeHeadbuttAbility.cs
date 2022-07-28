﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHeadbuttAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    private bool notHit;
    private void OnEnable()
    {
        notHit = true;
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = HeadbuttFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && notHit)
        {
            notHit = false;
            DamageReceiver receiver = collider.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
            AfterHit(collider);
        }
    }

    private float HeadbuttFormula(Ability ability)
    {
        int strength = caster.GetComponent<StateMachine>().stats.combatStats.Strength;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(strength * scalingCoeficient);
    }

    private void Update()
    {
        if (caster != null)
            transform.position = caster.transform.position;
    }
}