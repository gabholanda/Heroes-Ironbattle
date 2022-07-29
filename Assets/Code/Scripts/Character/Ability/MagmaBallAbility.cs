using UnityEngine;

public class MagmaBallAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;

    private void OnEnable()
    {
        newHandler = (ProjectileHandler)handler;
        damageDealer = GetComponent<DamageDealer>();
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
            damageDealer.DealDamage(this, caster);
            onHitParticles.Play();
            source.clip = handler.GetAbilityData().onHitSound;
            source.Play();
            AfterHit(collider);
        }
    }

}
