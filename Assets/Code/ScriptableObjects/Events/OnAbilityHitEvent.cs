using UnityEngine;

public abstract class OnAbilityHitEvent : ScriptableObject
{
    public abstract void Raise(GameObject caster, Collider2D collision);
}
