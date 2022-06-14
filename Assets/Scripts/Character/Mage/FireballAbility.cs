using UnityEngine;

public class FireballAbility : Ability
{
    ProjectileHandler newHandler;
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    [SerializeField]
    private GameObject burnEffectPrefab;
    [SerializeField]
    private float burnScalingCoeficient;


    private void OnEnable()
    {
        newHandler = (ProjectileHandler)handler;
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = FireballFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
    }
    private void Update()
    {
        rb.AddForce(newHandler.dir * newHandler.projectileSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
            SetStatusEffect(collider.gameObject);
        }
    }

    private float FireballFormula(Ability ability)
    {
        int intelligence = caster.GetComponent<StateMachine>().stats.combatStats.Intelligence;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(intelligence * scalingCoeficient);
    }

    private void SetStatusEffect(GameObject target)
    {
        Burn burn = target.GetComponentInChildren<Burn>();
        if (DoesNotContainEffect(burn))
        {
            GameObject burnObj = Instantiate(burnEffectPrefab, target.transform);
            burn = burnObj.GetComponent<Burn>();
            burn.target = target;
            burn.element = ElementType.Fire;
            burn.type = DamageType.Magical;
            burn.duration = 3;
            int intelligence = caster.GetComponent<StateMachine>().stats.combatStats.Intelligence;
            burn.effectValue = Mathf.Round(intelligence * burnScalingCoeficient);
            burn.Apply();
        }
        else
        {
            burn.Renew();
        }
    }
}
