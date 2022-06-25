using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderlightningAbility : Ability
{
    private DamageDealer damageDealer;
    private DamageFormula damageHandler;
    private DamageResources dealerHandler;
    [SerializeField]
    private GameObject freezeStatusEffectPrefab;
    [SerializeField]
    private float freezeScalingCoeficient;


    private void OnEnable()
    {
        damageDealer = GetComponent<DamageDealer>();
        damageHandler = ThunderLightningFormula;
        dealerHandler = DamageMethods.StandardDamageDealing;
        source = GetComponent<AudioSource>();
        source.clip = handler.GetAbilityData().onCastSound;
        source.Play();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            DamageReceiver receiver = collider.gameObject.GetComponent<DamageReceiver>();
            damageDealer.SetReceiver(receiver);
            StartCoroutine(DamageThrice());
            SetStatusEffect(collider.gameObject);
        }
    }


    private float ThunderLightningFormula(Ability ability)
    {
        int intelligence = caster.GetComponent<StateMachine>().stats.combatStats.Intelligence;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(intelligence * scalingCoeficient);
    }

    private void SetStatusEffect(GameObject target)
    {
        Slow slow = target.GetComponentInChildren<Slow>();
        if (DoesNotContainEffect(slow))
        {
            GameObject slowObj = Instantiate(freezeStatusEffectPrefab, target.transform);
            slow = slowObj.GetComponent<Slow>();
            slow.target = target;
            slow.element = ElementType.Ice;
            slow.type = DamageType.Magical;
            slow.duration = 5f;
            slow.effectValue = freezeScalingCoeficient;
            slow.Apply();
        }
        else
        {
            slow.Renew();
        }
    }

    private IEnumerator DamageThrice()
    {
        onHitParticles.Play();
        source.clip = handler.GetAbilityData().onHitSound;
        source.Play();
        damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
        yield return new WaitForSeconds(0.1f);
        damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
        yield return new WaitForSeconds(0.1f);
        damageDealer.DealDamage(GetComponent<Ability>(), damageHandler, dealerHandler);
    }
}
