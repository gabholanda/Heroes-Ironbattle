using UnityEngine;

public class ManaBlastAbility : Ability
{
    private DamageDealer damageDealer;

    public float radius;
    public float radiusIncreasePerIteration;

    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        source = GetComponent<AudioSource>();
        source.clip = handler.GetAbilityData().onCastSound;
        source.Play();
    }

    private void TriggerContact(Collider2D collider)
    {
        if (collider.gameObject.tag != caster.tag)
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(this, caster);
            AfterHit(collider);
        }
    }

    public void OverlapArea()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        radius += radiusIncreasePerIteration;

        for (int i = 0; i < colliders.Length; i++)
        {
            TriggerContact(colliders[i]);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
