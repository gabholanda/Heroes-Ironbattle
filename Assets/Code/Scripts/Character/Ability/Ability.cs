using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityHandler handler;
    public Rigidbody2D rb;
    public GameObject caster;
    public ParticleSystem onHitParticles;
    public GameEventListener listener;
    public AudioSource source;
    public GameEvent OnHitEvent;

    private void Awake()
    {
        OnHitEvent = ScriptableObject.CreateInstance<GameEvent>();
    }

    public void StartTimer()
    {
        if (handler.GetAbilityData().abilityDestroyDuration == 0f) return;
        Destroy(gameObject, handler.GetAbilityData().abilityDestroyDuration);
    }

    public virtual void SetupAbility(GameObject _caster)
    {
        listener = GetComponent<GameEventListener>();
        if (listener)
        {
            listener.Event = OnHitEvent;
            listener.TryRegister();
        }
        caster = _caster;
        handler.isCoolingDown = true;
        if (handler.coRunner)
            handler.coRunner.Run(handler.StartCooldown());
        this.StartTimer();
    }

    public virtual void AfterSetup()
    {

    }

    public virtual void AfterHit(Collider2D target)
    {
        if (listener != null)
        {

            handler?.effectList?.ForEach(effect =>
            {
                AbilityCallback abilityCallback = new AbilityCallback()
                .AddCaster(caster)
                .AddTarget(target)
                .AddTriggerOnTargetCallback(effect.Raise);
                listener.Response.AddListener(abilityCallback.TriggerTargetCallback);
            });

            handler?.abilitiesToTriggerOnHit?.ForEach(ability =>
            {
                AbilityCallback abilityCallback = new AbilityCallback()
                .AddCaster(caster)
                .AddPosition(target.transform.position)
                .AddTriggerAbilityCallback(ability.Execute);
                listener.Response.AddListener(abilityCallback.TriggerAbilityCallback);
            });
            OnHitEvent?.Raise();
        }
    }

}
