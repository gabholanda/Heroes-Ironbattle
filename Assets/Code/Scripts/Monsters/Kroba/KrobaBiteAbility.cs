using UnityEngine;

public class KrobaBiteAbility : Ability
{
    private DamageDealer damageDealer;
    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
    }

    public void AdjustPosition()
    {
        if (caster)
            transform.position = caster.GetComponent<GoblinStateMachine>().HitPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHit(collider))
        {
            DamageReceiver receiver = collider.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(this, caster);
            AfterHit(collider);
        }
    }


    public override void AfterSetup()
    {
        AdjustPosition();
    }
}
