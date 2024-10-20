using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbility : Ability
{
    private DamageDealer damageDealer;
    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHit(collider))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(this, caster);
            source.clip = handler.GetAbilityData().onHitSound;
            source.Play();
            AfterHit(collider);
        }
    }

}
