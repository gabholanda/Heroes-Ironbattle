using System.Collections;
using UnityEngine;

public class ThunderlightningAbility : Ability
{
    private DamageDealer damageDealer;

    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        source = GetComponent<AudioSource>();
        source.clip = handler.GetAbilityData().onCastSound;
        source.Play();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHit(collider))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            StartCoroutine(DamageThrice());
            AfterHit(collider);
        }
    }

    private IEnumerator DamageThrice()
    {
        onHitParticles.Play();
        source.clip = handler.GetAbilityData().onHitSound;
        source.Play();
        damageDealer.DealDamage(this, caster);
        yield return new WaitForSeconds(0.1f);
        damageDealer.DealDamage(this, caster);
        yield return new WaitForSeconds(0.1f);
        damageDealer.DealDamage(this, caster);
    }
}
