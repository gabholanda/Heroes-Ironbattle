using UnityEngine;

public class SlimeHeadbuttAbility : Ability
{
    private DamageDealer damageDealer;

    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            DamageReceiver receiver = collider.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(this, caster);
            AfterHit(collider);
        }
    }

    private void Update()
    {
        if (caster != null)
            transform.position = caster.transform.position;
    }
}
