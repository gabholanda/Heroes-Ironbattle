using UnityEngine;

public class ManaBlastAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;

    public float radius;
    public float radiusIncreasePerIteration;

    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = ManaBlastFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
        source = GetComponent<AudioSource>();
        source.clip = handler.GetAbilityData().onCastSound;
        source.Play();
    }

    private void TriggerContact(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
            AfterHit(collider);
        }
    }

    private float ManaBlastFormula(Ability ability)
    {
        CombatStats stats = caster.GetComponent<StateMachine>().stats.combatStats;
        int attribute = Mathf.Max(stats.Dexterity, stats.Strength, stats.Intelligence);
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(attribute * scalingCoeficient);
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
