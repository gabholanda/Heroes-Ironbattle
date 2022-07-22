using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityHandler handler;
    public Rigidbody2D rb;
    public GameObject caster;
    public ParticleSystem onHitParticles;
    public AudioSource source;

    public void StartTimer()
    {
        if (handler.GetAbilityData().abilityDestroyDuration == 0) return;
        Destroy(gameObject, handler.GetAbilityData().abilityDestroyDuration);
    }

    public virtual void SetupAbility(GameObject _caster)
    {
        caster = _caster;
        handler.isCoolingDown = true;
        if (handler.coRunner)
            handler.coRunner.Run(handler.StartCooldown());
        this.StartTimer();
    }

}
