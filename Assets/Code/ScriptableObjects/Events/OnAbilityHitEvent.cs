using UnityEngine;

public abstract class OnAbilityHitEvent : ScriptableObject
{
    public abstract void Raise(Collider2D collision);

    public abstract void Raise(Collider2D collision, GameObject caster);
}
