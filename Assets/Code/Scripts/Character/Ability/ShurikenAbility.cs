using UnityEngine;

public class ShurikenAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    private Vector2 casterDir;

    private void OnEnable()
    {
        newHandler = (ProjectileHandler)handler;
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = ShurikenFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
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
        if (collider.gameObject.CompareTag("Monster"))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
            source.clip = handler.GetAbilityData().onHitSound;
            source.Play();
            AfterHit(collider);
        }
    }

    private float ShurikenFormula(Ability ability)
    {
        CombatStats stats = caster.GetComponent<StateMachine>().stats.combatStats;
        int attribute = Mathf.Max(stats.Dexterity, stats.Strength, stats.Intelligence);
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(attribute * scalingCoeficient);
    }

    public override void SetupAbility(GameObject caster)
    {
        base.SetupAbility(caster);
        casterDir = caster.transform.localScale;
    }
}
