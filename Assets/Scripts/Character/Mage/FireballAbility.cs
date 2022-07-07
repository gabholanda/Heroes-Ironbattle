using UnityEngine;

public class FireballAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;

    private void OnEnable()
    {
        newHandler = (ProjectileHandler)handler;
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = FireballFormula;
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

    private float FireballFormula(Ability ability)
    {
        int intelligence = caster.GetComponent<StateMachine>().stats.combatStats.Intelligence;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(intelligence * scalingCoeficient);
    }

}
