using UnityEngine;


public delegate Ability TriggerAbilityCallback(GameObject g, Vector2 v2);
public delegate void TriggerOnTargetCallback(GameObject g, Collider2D t);
public class AbilityCallback
{
    private GameObject caster;
    private Collider2D target;
    private Vector2 position;
    private TriggerAbilityCallback callback;
    private TriggerOnTargetCallback targetCallback;

    public void TriggerAbilityCallback()
    {
        Ability ability = this.callback(caster, position);
        ability.AfterSetup();
    }

    public void TriggerTargetCallback()
    {
        this.targetCallback(caster, target);
    }

    public AbilityCallback AddCaster(GameObject _caster)
    {
        caster = _caster;
        return this;
    }

    public AbilityCallback AddTarget(Collider2D _target)
    {
        target = _target;
        return this;
    }

    public AbilityCallback AddPosition(Vector2 v2)
    {
        position = v2;
        return this;
    }

    public AbilityCallback AddTriggerAbilityCallback(TriggerAbilityCallback cb)
    {
        callback = cb;
        return this;
    }

    public AbilityCallback AddTriggerOnTargetCallback(TriggerOnTargetCallback cb)
    {
        targetCallback = cb;
        return this;
    }
}
