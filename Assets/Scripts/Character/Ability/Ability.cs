using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityHandler handler;
    public Rigidbody2D rb;
    public GameObject caster;
    public ParticleSystem onHitParticles;
    public AudioSource source;

    protected IEnumerator StartSelfDestroyTimer()
    {
        float currentTime = 0f;
        if (handler.GetAbilityData().abilityDestroyDuration == 0)
            yield return null;

        while (handler.GetAbilityData().abilityDestroyDuration > currentTime)
        {
            yield return new WaitForSeconds(0.1f);
            currentTime += 0.1f;
        }
        Destroy(gameObject);
    }

    public void StartTimers()
    {
        StartCoroutine(this.StartSelfDestroyTimer());
    }

    public virtual void SetupAbility(GameObject _caster)
    {
        caster = _caster;
        handler.isCoolingDown = true;
        handler.coRunner?.Run(handler.StartCooldown());
        this.StartTimers();
    }

}
