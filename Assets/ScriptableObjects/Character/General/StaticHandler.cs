using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Static Handler", menuName = "ScriptableObjects/Ability Handlers/New Static Handler")]
public class StaticHandler : AbilityHandler
{
    public override void Initialize(GameObject caster, Vector2 v2)
    {
        this.isCoolingDown = false;
        this.coRunner = caster.GetComponentInChildren<CoroutineRunner>();
    }

    public override void Execute(GameObject caster, Vector2 v2)
    {

        GameObject obj = Instantiate(prefab, caster.transform);
        this.isCoolingDown = true;
        Ability ability = obj.GetComponent<Ability>();
        ability.caster = caster;
        ability.StartTimers();
        coRunner.Run(this.StartCooldown());
    }
}
