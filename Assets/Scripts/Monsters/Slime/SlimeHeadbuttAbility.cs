using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHeadbuttAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    private int notHit;
    private float forceScaling = 50f;
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
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            CharacterMovement movement = collision.GetComponent<CharacterMovement>();
            movement.SetVector(new Vector2(0, 0));
            playerRb.velocity *= 0;
            playerRb.AddForce(GetRandomForce() * forceScaling, ForceMode2D.Impulse);
            DamageReceiver receiver = collision.GetComponent<DamageReceiver>();
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

    private Vector3 GetRandomForce()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        return new Vector3(x, y, 0).normalized;
    }

    private void Update()
    {
        if (caster != null)
            transform.position = caster.transform.position;
    }
}
