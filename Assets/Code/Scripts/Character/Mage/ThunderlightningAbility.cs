using System.Collections;
using UnityEngine;

public class ThunderlightningAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;

    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = ThunderLightningFormula;
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
            StartCoroutine(DamageThrice());
            AfterHit(collider);
        }
    }


    private float ThunderLightningFormula(Ability ability)
    {
        int intelligence = caster.GetComponent<StateMachine>().stats.combatStats.Intelligence;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(intelligence * scalingCoeficient);
    }

    private IEnumerator DamageThrice()
    {
        onHitParticles.Play();
        source.clip = handler.GetAbilityData().onHitSound;
        source.Play();
        damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
        yield return new WaitForSeconds(0.1f);
        damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
        yield return new WaitForSeconds(0.1f);
        damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
    }
}
