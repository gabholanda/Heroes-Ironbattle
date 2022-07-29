using UnityEngine;

public class DarkSlashAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;

    private void OnEnable()
    {
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
            source.clip = handler.GetAbilityData().onHitSound;
            source.Play();
            onHitParticles.Play();
            AfterHit(collider);
        }
    }

    public override void AfterSetup()
    {
        newHandler = (ProjectileHandler)handler;
    }
}
