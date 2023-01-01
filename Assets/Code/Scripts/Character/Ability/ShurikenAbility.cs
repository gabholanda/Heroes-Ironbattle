using UnityEngine;

public class ShurikenAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;
    private Vector2 casterDir;

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
        rb.AddForce(newHandler.projectileSpeed * Time.fixedDeltaTime * newHandler.dir * casterDir);
        transform.Rotate(new Vector3(0, 0, -30));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != caster.tag)
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(this, caster);
            source.clip = handler.GetAbilityData().onHitSound;
            source.Play();
            AfterHit(collider);
        }
    }

    public override void SetupAbility(GameObject caster)
    {
        base.SetupAbility(caster);
        casterDir = caster.transform.localScale;
    }
}
