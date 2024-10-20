using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multi Cast Handler", menuName = "ScriptableObjects/Ability Handlers/New Multi Cast Handler")]
public class MultiCastHandler : AbilityHandler
{
    public List<AbilityHandler> abilities;
    public override void Initialize(GameObject caster)
    {
        this.isCoolingDown = false;
        this.coRunner = caster.GetComponent<CoroutineRunner>();
    }
    public override Ability Execute(GameObject caster, Vector2 v2)
    {
        abilities.ForEach((handler) =>
        {
            handler.Initialize(caster);
            handler.Execute(caster, v2);
        });

        return null;
    }
}
